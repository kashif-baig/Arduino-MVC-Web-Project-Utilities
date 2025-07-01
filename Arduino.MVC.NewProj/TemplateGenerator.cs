using Arduino.MVC.NewProj.Properties;
using System.Text;

namespace Arduino.MVC.NewProj
{
    internal class TemplateGenerator
    {
        public TemplateGenerator(Options options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options;
        }

        public Options Options { get; }

        public void Generate()
        {
            MakeFolders();
            CreateStaticFiles();
            CreateLayoutTemplate();
            CreateLayoutTemplateClass();
            CreateController();
        }

        private void MakeFolders()
        {
            if (Directory.Exists(Options.ProjectFolderPath))
            {
                throw new Exception($"'{Options.ProjectFolderPath}' already exists.");
            }

            Directory.CreateDirectory(Options.ASPFilesFolderPath);
            Directory.CreateDirectory(Options.VSCodeFolderPath);

        }

        private void CreateStaticFiles()
        {
            File.WriteAllText($"{Options.ProjectFolderPath}Content.h",Resources.Content_h);
            File.WriteAllText($"{Options.ProjectFolderPath}ILayoutContentView.h", Resources.ILayoutContentView_h);

            File.WriteAllText($"{Options.VSCodeFolderPath}arduino.json", Resources.arduino_json.Replace(
                PlaceHolderName.ProjectName, Options.ProjectName));

            File.WriteAllText($"{Options.VSCodeFolderPath}BuildViews.ps1",Resources.BuildViews_script);

            StringBuilder heading = new StringBuilder();

            if (Options.AddAuthenticator)
            {
                heading.Clear();
                heading.Append(Resources.Heading_ViewSource);
                heading.Replace(PlaceHolderName.ViewName, "Login");

                File.WriteAllText($"{Options.ASPFilesFolderPath}LoginView.asp", Resources.LoginView_asp
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    );

                heading.Clear();
                heading.Append(Resources.Heading_ViewClass);

                File.WriteAllText($"{Options.ProjectFolderPath}LoginView.h", Resources.LoginView_h
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    );

                File.WriteAllText($"{Options.ProjectFolderPath}LoginModel.h", Resources.LoginModel_h);
                File.WriteAllText($"{Options.ProjectFolderPath}Authenticator.h", Resources.Authenticator_h);
            }

            File.WriteAllText($"{Options.ProjectFolderPath}{Options.ProjectName}.ino", Resources.Project_ino
                .Replace(PlaceHolderName.AuthCheck, Options.AddAuthenticator ? "" : "//"));

            foreach (var viewOption in Options.ViewOptions)
            {
                heading.Clear();
                heading.Append(Resources.Heading_ViewSource);
                heading.Replace(PlaceHolderName.ViewName, viewOption.ViewName);

                File.WriteAllText($"{Options.ASPFilesFolderPath}{viewOption.ViewName}View.asp", Resources.View_asp
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );

                heading.Clear();
                heading.Append(Resources.Heading_ViewClass);

                File.WriteAllText($"{Options.ProjectFolderPath}{viewOption.ViewName}View.h", Resources.View_h
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );

                File.WriteAllText($"{Options.ProjectFolderPath}{viewOption.ViewName}Model.h", Resources.Model_h
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );
            }

            foreach (var viewOption in Options.ApiOptions)
            {
                heading.Clear();
                heading.Append(Resources.Heading_ViewSource);
                heading.Replace(PlaceHolderName.ViewName, viewOption.ViewName);

                File.WriteAllText($"{Options.ASPFilesFolderPath}{viewOption.ViewName}View.asp", Resources.Api_asp
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );

                heading.Clear();
                heading.Append(Resources.Heading_ViewClass);

                File.WriteAllText($"{Options.ProjectFolderPath}{viewOption.ViewName}View.h", Resources.Api_h
                    .Replace(PlaceHolderName.HeadingText, heading.ToString())
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );

                File.WriteAllText($"{Options.ProjectFolderPath}{viewOption.ViewName}Model.h", Resources.Model_h
                    .Replace(PlaceHolderName.ViewName, viewOption.ViewName)
                    .Replace(PlaceHolderName.ViewNameUpper, viewOption.ViewName.ToUpper())
                    );
            }
        }

        private void CreateLayoutTemplate()
        {
            StringBuilder viewLinks = new StringBuilder();

            foreach (var viewOption in Options.ViewOptions)
            {
                viewLinks.AppendLine($"                <a href=\"/{viewOption.ViewName}\">{viewOption.ViewName}</a>");
            }

            if (Options.AddAuthenticator)
            {
                viewLinks.AppendLine($"                <a href=\"/Login\">Logout <%: request->getAuthenticatedUser() %></a>");
            }

            StringBuilder heading = new StringBuilder();
            heading.Append(Resources.Heading_ViewSource);
            heading.Replace(PlaceHolderName.ViewName, "LayoutTemplate");

            string projectNameCleaned = Options.ProjectName.Replace('_', ' ').Trim();
            StringBuilder layoutTemplate = new StringBuilder(Resources.LayoutTemplateView_asp);

            layoutTemplate.Replace(PlaceHolderName.HeadingText, heading.ToString());
            layoutTemplate.Replace(PlaceHolderName.ProjectName, projectNameCleaned);
            layoutTemplate.Replace(PlaceHolderName.AuthCheck, Options.AddAuthenticator ? "" : "// ");
            layoutTemplate.Replace(PlaceHolderName.ViewLinks, viewLinks.ToString());

            File.WriteAllText($"{Options.ASPFilesFolderPath}LayoutTemplateView.asp", layoutTemplate.ToString());
        }

        private void CreateLayoutTemplateClass()
        {
            StringBuilder viewLinks = new StringBuilder();

            foreach (var viewOption in Options.ViewOptions)
            {
                viewLinks.Append($"<a href=\\\"/{viewOption.ViewName}\\\">{viewOption.ViewName}</a>");
            }
            StringBuilder writeStatements = new StringBuilder();
            writeStatements.AppendLine($"\t\twriteStr(PSTR(\"{viewLinks}\"));");
            
            if (Options.AddAuthenticator)
            {
                writeStatements.AppendLine("\t\twriteStr(PSTR(\"<a href=\\\"/Login\\\">Logout \")); response->print(request->getAuthenticatedUser()); writeStr(PSTR(\"</a>\"));");
            }
            string projectNameCleaned = Options.ProjectName.Replace('_', ' ').Trim();

            StringBuilder heading = new StringBuilder();
            heading.Append(Resources.Heading_ViewClass);

            StringBuilder layoutTemplate = new StringBuilder(Resources.LayoutTemplateView_h);
            layoutTemplate.Replace(PlaceHolderName.HeadingText, heading.ToString());
            layoutTemplate.Replace(PlaceHolderName.ProjectName, projectNameCleaned);
            layoutTemplate.Replace(PlaceHolderName.AuthCheck, Options.AddAuthenticator ? "" : "// ");
            layoutTemplate.Replace(PlaceHolderName.ViewLinks, writeStatements.ToString());

            File.WriteAllText($"{Options.ProjectFolderPath}LayoutTemplateView.h", layoutTemplate.ToString());
        }

        private void CreateController()
        {
            StringBuilder includes = new StringBuilder();
            StringBuilder sizeOfViews = new StringBuilder();
            StringBuilder sizeOfModels = new StringBuilder();
            StringBuilder staticMaxBrackets = new StringBuilder();
            StringBuilder serveView = new StringBuilder();

            if (Options.AddAuthenticator)
            {
                includes.AppendLine("#include \"Authenticator.h\"");
                includes.AppendLine("#include \"LoginView.h\"");
                sizeOfViews.AppendLine("    STATIC_MAX(sizeof(LoginView),");
                sizeOfModels.AppendLine("    STATIC_MAX(sizeof(LoginModel),");

                staticMaxBrackets.Append(")");
            }

            foreach (var viewOption in Options.ViewOptions.Union(Options.ApiOptions))
            {
                includes.AppendLine($"#include \"{viewOption.ViewName}View.h\"");
                sizeOfViews.AppendLine($"    STATIC_MAX(sizeof({viewOption.ViewName}View),");
                sizeOfModels.AppendLine($"    STATIC_MAX(sizeof({viewOption.ViewName}Model),");
                staticMaxBrackets.Append(")");
            }

            int viewCounter = 0;

            foreach (var viewOption in Options.ViewOptions)
            {
                string defaultCheck = viewCounter == 0 ? "argc == 0 || " : string.Empty;

                serveView.AppendLine(Resources.Controller_ServeView);
                serveView.Replace(PlaceHolderName.ViewName, viewOption.ViewName);
                serveView.Replace(PlaceHolderName.ViewNameCamel, viewOption.ViewNameCamel);
                serveView.Replace(PlaceHolderName.DefaultCheck, defaultCheck);
                viewCounter++;
            }

            StringBuilder serveApi = new StringBuilder();

            foreach (var apiOption in Options.ApiOptions)
            {
                serveApi.AppendLine(Resources.Controller_ServeApi);
                serveApi.Replace(PlaceHolderName.ViewNameStripped, apiOption.ViewNameNoApiSuffix);
                serveApi.Replace(PlaceHolderName.ViewName, apiOption.ViewName);
                serveApi.Replace(PlaceHolderName.ViewNameCamel, apiOption.ViewNameCamel);
            }

            StringBuilder controller = new StringBuilder(Resources.Controller_h);
            controller.Replace(PlaceHolderName.Includes, includes.ToString());
            controller.Replace(PlaceHolderName.SizeofView, sizeOfViews.ToString());
            controller.Replace(PlaceHolderName.SizeofModel, sizeOfModels.ToString());
            controller.Replace(PlaceHolderName.StaticMaxBrackets, staticMaxBrackets.ToString());

            controller.Replace(PlaceHolderName.Controller_OnAuthenticate, Options.AddAuthenticator ? Resources.Controller_onAuth : String.Empty);
            controller.Replace(PlaceHolderName.Controller_IsAuthenticated, Options.AddAuthenticator ? Resources.Controller_isAuthenticated : String.Empty);
            controller.Replace(PlaceHolderName.AuthCheck, !Options.AddAuthenticator ? "//" : String.Empty);

            controller.Replace(PlaceHolderName.Controller_ServeView, serveView.ToString());

            if (Options.ApiOptions.Count != 0)
            {
                StringBuilder apiCheck = new StringBuilder(Resources.Controller_ApiCheck);
                apiCheck.Replace(PlaceHolderName.Controller_ServeApi, serveApi.ToString());
                controller.Replace(PlaceHolderName.Controller_ApiCheck, apiCheck.ToString());
            }
            else
            {
                controller.Replace(PlaceHolderName.Controller_ApiCheck, String.Empty);
            }

            File.WriteAllText($"{Options.ProjectFolderPath}Controller.h", controller.ToString());
        }

    }
}
