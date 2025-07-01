using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Arduino.MVC.CodeGen.Properties;

namespace Arduino.MVC.CodeGen
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine($"Usage: {typeof(Program).Assembly.GetName().Name} <asp folder path> <destination folder path>");
                    return -1;
                }
                string sourceDir = args[0];
                string destDir = args[1];

                if (!Path.IsPathFullyQualified(sourceDir))
                {
                    sourceDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, sourceDir));
                }
                sourceDir = sourceDir.TrimEnd('\\', '/') + Path.DirectorySeparatorChar;

                if (!Path.IsPathFullyQualified(destDir))
                {
                    destDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, destDir));
                }
                destDir = destDir.TrimEnd('\\', '/') + Path.DirectorySeparatorChar;

                List<string> generatedViews = new List<string>();

                foreach (var filePath in Directory.EnumerateFiles(sourceDir, "*.asp"))
                {
                    DateTime sourceFileModifyDate = File.GetLastWriteTime(filePath);

                    var filename = Path.GetFileNameWithoutExtension(filePath);
                    string outputCodePath = $"{destDir}{filename}.h";

                    bool outputCodeFileExists = File.Exists(outputCodePath);
                    DateTime outputCodeFileModifyDate = DateTime.MinValue;

                    if (outputCodeFileExists)
                    {
                        outputCodeFileModifyDate = File.GetLastWriteTime(outputCodePath);
                    }

                    if (outputCodeFileExists && outputCodeFileModifyDate > sourceFileModifyDate)
                    {
                        continue;
                    }
                    string contentText = File.ReadAllText(filePath);

                    using (MemoryStream contentOutput = new MemoryStream())
                    {
                        StringBuilder codeOutput = new StringBuilder();
                        using (StreamWriter sw = new StreamWriter(contentOutput))
                        {
                            string viewName;
                            bool inMemory;

                            ViewCodeGenerator.GenerateCodeAndContent(contentText, Resources.CppCodeInfo, out viewName, out inMemory, codeOutput, sw);

                            File.WriteAllText(outputCodePath, codeOutput.ToString());
                        }
                        generatedViews.Add(filename);
                    }
                }

                if (generatedViews.Count > 0)
                {
                    Console.WriteLine("Generating views ...");
                    foreach (var fileName in generatedViews)
                    {
                        Console.WriteLine($"{fileName}.h");
                    }
                    Console.WriteLine("View generation complete.");
                }
                else
                {
                    Console.WriteLine("No views generated/updated.");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
