using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Arduino.MVC
{
    public enum InputFsm
    {
        Content,
        OpenBracket,
        OpenPercent,
        Code,
        CodeScalar,
        CodeScalarClosePercent,
        ClosePercent,
    }

    public class ViewCodeGenerator
    {
        const string ValidIdentifier = "^[a-zA-Z][a-zA-Z0-9]*$";

        public static void GenerateCodeAndContent(string contentInputText, string description, out string viewName,
            out bool inMemory, StringBuilder codeOutput, StreamWriter streamOutput)
        {
            const string V1Compat = "@v1-compat";
            const string ModelTypeString = "@modeltype ";
            const string ContentTypeString = "@contenttype ";
            const string MimeTypeString = "@mimetype ";
            const string ViewNameString = "@viewname ";
            const string IncludeString = "@include ";
            const string RemString = "@rem";
            const string ProgmemString = "@progmem ";
            const string MinifyString = "@minify ";
            const string ViewIdString = "@viewid ";
            const string ViewBaseString = "@viewbase ";
            const string RenderSectionString = "@content ";
            const string WriteStrString = "@writestr ";

            viewName = string.Empty;
            inMemory = true;
            string avrProgMem = "PROGMEM ";
            string modelType = string.Empty;
            string contentType = string.Empty;
            string mimeType = string.Empty;
            bool viewIdSpecified = false;
            string viewId = string.Empty;
            string viewBase = "IView";
            bool useWriteStr = true;

            bool minify = true;
            bool isV1CompatMode = false;

            var includes = new List<string>();

            StringBuilder preProcessedInput = new StringBuilder();
            List<RenderSection> renderSections = new List<RenderSection>(8);

            using (var sr = new StringReader(contentInputText))
            {
                var line = sr.ReadLine();

                // identity directives
                while (line != null)
                {
                    line = line.Trim();
                    if (line.StartsWith(ModelTypeString, true, CultureInfo.CurrentCulture) && line.Length > ModelTypeString.Length)
                    {
                        modelType = line.Substring(ModelTypeString.Length).Trim();
                    }
                    else if (line.StartsWith(ContentTypeString, true, CultureInfo.CurrentCulture) && line.Length > ContentTypeString.Length)
                    {
                        if (string.IsNullOrEmpty(contentType))
                        {
                            contentType = line.Substring(ContentTypeString.Length).Trim().ToLower();
                        }
                    }
                    else if (line.StartsWith(MimeTypeString, true, CultureInfo.CurrentCulture) && line.Length > MimeTypeString.Length)
                    {
                        if (string.IsNullOrEmpty(mimeType))
                        {
                            mimeType = line.Substring(MimeTypeString.Length).Trim();
                            contentType = mimeType.ToLower();
                        }
                    }
                    else if (line.StartsWith(ViewNameString, true, CultureInfo.CurrentCulture) && line.Length > ViewNameString.Length)
                    {
                        viewName = line.Substring(ViewNameString.Length).Trim();

                        if (viewName.Length > 24)
                        {
                            throw new FormatException(string.Format("View name '{0}' exceeds max 24 chars.", viewName));
                        }
                        if (!Regex.IsMatch(viewName, ValidIdentifier))
                        {
                            throw new FormatException(string.Format("View name '{0}' is not in the correct format.", viewName));
                        }
                    }
                    else if (line.StartsWith(IncludeString, true, CultureInfo.CurrentCulture) && line.Length > IncludeString.Length)
                    {
                        includes.Add($"#include {line.Substring(IncludeString.Length).Trim()}");
                    }
                    else if (line.StartsWith(RemString, true, CultureInfo.CurrentCulture))
                    {

                    }
                    else if (line.StartsWith(ProgmemString, true, CultureInfo.CurrentCulture) && line.Length > ProgmemString.Length)
                    {
                        string avrMem = line.Substring(ProgmemString.Length).Trim().ToLower();
                        if (avrMem != "true")
                        {
                            avrProgMem = "";
                        }
                    }
                    else if (line.StartsWith(MinifyString, true, CultureInfo.CurrentCulture) && line.Length > MinifyString.Length)
                    {
                        string minifyVal = line.Substring(MinifyString.Length).Trim().ToLower();

                        if (minifyVal != "true")
                        {
                            minify = false;
                        }
                    }
                    else if (line == V1Compat)
                    {
                        isV1CompatMode = true;
                    }
                    else if (line.StartsWith(ViewIdString, true, CultureInfo.CurrentCulture) && line.Length > ViewIdString.Length)
                    {
                        string viewIdVal = line.Substring(ViewIdString.Length).Trim();

                        if (viewIdVal == "false")
                        {
                            viewIdSpecified = false;
                        }
                        else if (viewIdVal == "true")
                        {
                            viewIdSpecified = true;
                        }
                        else if (viewIdVal.Length > 0)
                        {
                            viewIdSpecified = true;
                            viewId = viewIdVal;
                        }
                    }
                    else if (line.StartsWith(WriteStrString, true, CultureInfo.CurrentCulture) && line.Length > WriteStrString.Length)
                    {
                        string writeStrVal = line.Substring(WriteStrString.Length).Trim();

                        if (writeStrVal == "true")
                        {
                            useWriteStr = true;
                        }
                        else
                        {
                            useWriteStr = false;
                        }
                    }
                    else if (line.StartsWith(ViewBaseString, true, CultureInfo.CurrentCulture) && line.Length > ViewBaseString.Length)
                    {
                        string viewBaseVal = line.Substring(ViewBaseString.Length).Trim();

                        if (viewBaseVal.Length > 24)
                        {
                            throw new FormatException(string.Format("View base class name '{0}' exceeds max 24 chars.", viewBaseVal));
                        }
                        if (!Regex.IsMatch(viewBaseVal, ValidIdentifier))
                        {
                            throw new FormatException(string.Format("View base class name '{0}' is not in the correct format.", viewBaseVal));
                        }
                        viewBase = viewBaseVal;
                    }
                    // Exit loop if blank line
                    else if (line.Length == 0)
                    {
                        break;
                    }
                    line = sr.ReadLine();
                }

                if (string.IsNullOrEmpty(viewName))
                {
                    throw new InvalidOperationException("No ViewName specified. ViewName must be specified.");
                }

                // All directives have been identified, but remaining input content has yet to be read.
                // Read remaining input content, and minify if required.

                while (line != null)
                {
                    if (line.Trim().StartsWith(RenderSectionString, true, CultureInfo.CurrentCulture) && line.Length > RenderSectionString.Length)
                    {
                        string renderMethodName = line.Trim().Substring(RenderSectionString.Length).Trim();

                        if (renderMethodName.Length > 24)
                        {
                            throw new FormatException(string.Format("Render method name '{0}' exceeds max 24 chars.", renderMethodName));
                        }
                        if (!Regex.IsMatch(renderMethodName, ValidIdentifier))
                        {
                            throw new FormatException(string.Format("Render method name '{0}' is not in the correct format.", renderMethodName));
                        }

                        preProcessedInput = new StringBuilder();

                        // Add to list of render sections.
                        renderSections.Add(new RenderSection(renderMethodName, preProcessedInput));
                    }

                    else if (minify)
                    {
                        var lineTrimmed = line.Trim();
                        if (!string.IsNullOrEmpty(lineTrimmed))
                        {
                            preProcessedInput.Append(lineTrimmed);
                            preProcessedInput.Append("\n");
                        }
                    }
                    else
                    {
                        preProcessedInput.AppendLine(line);
                    }
                    line = sr.ReadLine();
                }
            }

            // All input content has been read. Perform further pre-processing if required.

            if (minify)
            {
                if (renderSections.Count() > 0)
                {
                    foreach (var renderSection in renderSections)
                    {
                        FinalPreProcessing(contentType, renderSection.SectionContent);
                    }
                }
                else
                {
                    FinalPreProcessing(contentType, preProcessedInput);
                }
            }

            // If not V1 compatible, force view file to be in memory.
            if (!isV1CompatMode)
            {
                inMemory = true;
            }
            else
            {
                // Force use of writeStr to false if V1 compatible.
                useWriteStr = false;
            }

            // Start generating code output file.
            codeOutput.AppendFormat("#ifndef {0}VIEW_H{1}", viewName.ToUpper(), Environment.NewLine);
            codeOutput.AppendFormat("#define {0}VIEW_H{1}", viewName.ToUpper(), Environment.NewLine);
            codeOutput.AppendLine();
            codeOutput.AppendLine(description);
            codeOutput.AppendLine();
            includes.Insert(0, $"#include \"{viewBase}.h\"");

            foreach (var include in includes)
            {
                codeOutput.AppendLine(include);
            }

            codeOutput.AppendLine();
            if (!useWriteStr)
            {
                codeOutput.AppendLine("{{viewfile}}");    // placeholder replaced further down in code.
            }
            codeOutput.AppendLine();

            // Generate class begin.
            codeOutput.AppendLine($"class {viewName}View: public {viewBase} {{");
            codeOutput.AppendLine("public:");

            // Generate the constructor code.
            GenerateConstructor(viewName, viewBase, inMemory, useWriteStr, codeOutput, modelType, mimeType, viewIdSpecified, viewId, isV1CompatMode);

            // Generate getModel() method
            if (!string.IsNullOrEmpty(modelType))
            {
                codeOutput.Append($"{modelType} getModel () const {{ return model;}}{Environment.NewLine}");
            }
            codeOutput.AppendLine("protected:");

            // Generate class attributes.
            if (!string.IsNullOrEmpty(modelType))
            {
                codeOutput.AppendFormat("{0} model;{1}{1}", modelType, Environment.NewLine);
            }

            // Generate render method(s).

            if (renderSections.Count() > 0)
            {
                foreach (var renderSection in renderSections)
                {
                    var preProcessedInputString = renderSection.SectionContent.ToString().Trim();
                    GenerateRenderMethod(codeOutput, streamOutput, $"render{renderSection.SectionName}", preProcessedInputString, inMemory, useWriteStr);
                }
            }
            else
            {
                var preProcessedInputString = preProcessedInput.ToString().Trim();
                GenerateRenderMethod(codeOutput, streamOutput, "onRender", preProcessedInputString, inMemory, useWriteStr);
            }

            // Generate class end.
            codeOutput.AppendLine("};");
            codeOutput.AppendLine();

            // Replace {{viewfile}} placeholder.
            if (inMemory)
            {
                if (!useWriteStr)
                {
                    Stream contentOutput = streamOutput.BaseStream;
                    var hexBytes = MemoryStreamToHexBytes(contentOutput);

                    var viewfile = string.Format("{0}const char {1}ViewFile[] = {{ {2} }};", avrProgMem, viewName, hexBytes);
                    codeOutput.Replace("{{viewfile}}", viewfile);
                }
            }
            else
            {
                string sdCardFileName = viewName.ToLower();

                if (sdCardFileName.Length > 8)
                {
                    sdCardFileName = sdCardFileName.Substring(0, 8);
                }

                var viewfile = string.Format("{0}const char {1}ViewFile[] = \"web/{2}.vw\";", avrProgMem, viewName, sdCardFileName);
                codeOutput.Replace("{{viewfile}}", viewfile);
            }
            codeOutput.AppendLine("#endif");

            // Code generation complete.
        }


        private static void FinalPreProcessing(string contentType, StringBuilder preProcessedInput)
        {
            preProcessedInput.Replace("\n<%", "<%");
            preProcessedInput.Replace("%>\n", "%>");

            if (contentType.EndsWith("html") || contentType.EndsWith("xml"))
            {
                preProcessedInput.Replace(">\n", ">");
            }
            else if (contentType.EndsWith("json"))
            {
                preProcessedInput.Replace("{\n", "{");
                preProcessedInput.Replace("}\n", "}");
                preProcessedInput.Replace("\n{", "{");
                preProcessedInput.Replace("\n}", "}");
                preProcessedInput.Replace(",\n", ",");
                preProcessedInput.Replace("]\n", "]");
                preProcessedInput.Replace("\"\n", "\"");
            }
        }


        private static void GenerateConstructor(string viewName, string viewBase, bool inMemory, bool useWriteStr, StringBuilder codeOutput, string modelType, string mimeType, bool viewIdSpecified, string viewId, bool isV1CompatMode)
        {
            string viewIdString = "ViewIdNotSpecified"; // = viewIdSpecified ? $"{viewName}ViewId" : "ViewIdNotSpecified";

            if (viewIdSpecified)
            {
                viewIdString = (viewId.Length > 0) ? viewId : $"{viewName}ViewId";
            }

            if (!isV1CompatMode)
            {
                string requestParam = "HttpRequest *request = getDefaultRequest()";
                string modelParam =!string.IsNullOrEmpty(modelType) ? modelType + " model, " + requestParam : requestParam;

                if (!useWriteStr)
                {
                    codeOutput.AppendLine($"{viewName}View ({modelParam}) : {viewBase}({viewIdString}, {viewName}ViewFile, request) {{");
                }
                else
                {
                    codeOutput.AppendLine($"{viewName}View ({modelParam}) : {viewBase}({viewIdString}, NULL, request) {{");
                }
            }
            else
            {
                string modelParam = !string.IsNullOrEmpty(modelType) ? ", " + modelType + " model" : string.Empty;
                codeOutput.AppendFormat("{0}View (Stream *responseStream{1}) {{\n",
                    viewName, modelParam);
            }
            if (!string.IsNullOrEmpty(modelType))
            {
                codeOutput.AppendLine("this->model = model;");
            }
            if (!string.IsNullOrEmpty(mimeType))
            {
                codeOutput.AppendFormat("this->viewMimeType = {0};", mimeType);
                codeOutput.AppendLine();
            }

            if (!isV1CompatMode)
            {

            }
            else
            {
                codeOutput.AppendLine("this->response = responseStream;;");
                codeOutput.AppendFormat("this->location = {0};\n", inMemory ? "ViewInMemory" : "ViewOnSDCard");
            }

            codeOutput.AppendLine("}");
        }


        private static void GenerateRenderMethod(StringBuilder codeOutput, StreamWriter streamOutput, string methodName, string preProcessedInputString, bool isInMemory, bool useWriteStr)
        {
            codeOutput.AppendLine($"void {methodName} () {{");

            // Prepare to read pre-processed input content character by character.

            InputFsm inputFsm = InputFsm.Content;

            StringReader inputChars = new StringReader(preProcessedInputString);
            Stream contentOutput = streamOutput.BaseStream;

            long contentByteStartPos = contentOutput.Length;
            int charInt;

            StringBuilder writeStrContent = new StringBuilder();

            while ((charInt = inputChars.Read()) > -1)
            {
                char currentChar = Convert.ToChar(charInt);

                switch (inputFsm)
                {
                    case InputFsm.Content:
                        if (currentChar == '<')
                        {
                            inputFsm = InputFsm.OpenBracket;
                        }
                        else
                        {
                            writeStrContent.Append(currentChar);

                            streamOutput.Write(currentChar);
                            streamOutput.Flush();
                        }
                        break;
                    case InputFsm.OpenBracket:
                        if (currentChar == '%')
                        {
                            inputFsm = InputFsm.OpenPercent;
                        }
                        else
                        {
                            if (currentChar != '<')
                            {
                                inputFsm = InputFsm.Content;
                                writeStrContent.Append("<");
                                streamOutput.Write('<');
                            }

                            writeStrContent.Append($"{currentChar}");
                            streamOutput.Write(currentChar);
                            streamOutput.Flush();
                        }
                        break;
                    case InputFsm.OpenPercent:
                        if (contentOutput.Length - 1 >= contentByteStartPos)
                        {
                            if (!useWriteStr)
                            {
                                codeOutput.AppendFormat("writeView{0}Content({1}, {2});{3}", isInMemory ? "" : "SD", contentByteStartPos,
                                    contentOutput.Length - 1, Environment.NewLine);
                            }
                            else
                            {
                                writeStrContent.Replace(@"\",@"\\");
                                writeStrContent.Replace("\n",@"\n");
                                writeStrContent.Replace("\r", @"\r");
                                writeStrContent.Replace("\t", @"\t");
                                writeStrContent.Replace("\"", "\\\"");

                                codeOutput.Append($"writeStr(PSTR(\"{writeStrContent.ToString()}\"));{Environment.NewLine}");

                                writeStrContent.Clear();
                            }
                        }

                        contentByteStartPos = contentOutput.Length;

                        if (currentChar == ':')
                        {
                            inputFsm = InputFsm.CodeScalar;
                            codeOutput.Append("response->print(");
                        }
                        else
                        {
                            inputFsm = InputFsm.Code;
                            codeOutput.Append(currentChar);
                        }

                        break;
                    case InputFsm.Code:
                        if (currentChar == '%')
                        {
                            inputFsm = InputFsm.ClosePercent;
                        }
                        else
                        {
                            codeOutput.Append(currentChar);
                        }
                        break;
                    case InputFsm.CodeScalar:
                        if (currentChar == '%')
                        {
                            inputFsm = InputFsm.CodeScalarClosePercent;
                        }
                        else
                        {
                            codeOutput.Append(currentChar);
                        }
                        break;
                    case InputFsm.CodeScalarClosePercent:
                        if (currentChar == '>')
                        {
                            inputFsm = InputFsm.Content;
                            codeOutput.AppendLine(");");
                        }
                        else
                        {
                            inputFsm = InputFsm.CodeScalar;
                            codeOutput.Append(Convert.ToChar('%'));
                            codeOutput.Append(currentChar);
                        }
                        break;
                    case InputFsm.ClosePercent:
                        if (currentChar == '>')
                        {
                            inputFsm = InputFsm.Content;
                            codeOutput.AppendLine();
                        }
                        else
                        {
                            inputFsm = InputFsm.Code;
                            codeOutput.Append(Convert.ToChar('%'));
                            codeOutput.Append(currentChar);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (inputFsm == InputFsm.Content)
            {
                if (contentOutput.Length - 1 >= contentByteStartPos)
                {
                    if (!useWriteStr)
                    {
                        codeOutput.AppendFormat("writeView{0}Content({1}, {2});{3}", isInMemory ? "" : "SD", contentByteStartPos, contentOutput.Length - 1,
                        Environment.NewLine);
                    }
                    else
                    {
                        writeStrContent.Replace(@"\", @"\\");
                        writeStrContent.Replace("\n", @"\n");
                        writeStrContent.Replace("\r", @"\r");
                        writeStrContent.Replace("\t", @"\t");
                        writeStrContent.Replace("\"", "\\\"");

                        codeOutput.Append($"writeStr(PSTR(\"{writeStrContent.ToString()}\"));{Environment.NewLine}");

                        writeStrContent.Clear();
                    }
                }
            }

            // render method end block
            codeOutput.AppendLine("}");
            codeOutput.AppendLine();
        }


        public static string ConvertToHexBytesString(string filename, byte[] filebytes)
        {
            var hexByteArray = (from b in filebytes select string.Format("0x{0:X2}", b));

            StringBuilder sb = new StringBuilder(32768);

            int lineItems = 0;
            int i = 0;
            foreach (var hexByte in hexByteArray)
            {
                if (i > 0) sb.Append(",");

                sb.Append(hexByte);

                lineItems++;
                if (lineItems >= 20)
                {
                    sb.AppendLine();
                    lineItems = 0;
                }
                i++;
            }

            return $"const char file_{filename.Replace(".", "_").Replace("-", "_")}[] = {{ {sb} }};";
        }

        static string MemoryStreamToHexBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var memoryStream = new MemoryStream((int)stream.Length))
            {
                stream.CopyTo(memoryStream);

                var hexByteArray = (from b in memoryStream.ToArray() select string.Format("0x{0:X2}", b));

                string hexBytes = string.Join(",", hexByteArray);
                return hexBytes;
            }
        }

    }

    public class RenderSection
    {
        public RenderSection(string sectionName, StringBuilder sectionContent)
        {
            SectionName = sectionName;
            SectionContent = sectionContent;
        }

        public string SectionName { get; }

        public StringBuilder SectionContent { get; }
    }
}
