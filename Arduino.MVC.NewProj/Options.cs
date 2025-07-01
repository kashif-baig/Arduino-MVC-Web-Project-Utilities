namespace Arduino.MVC.NewProj
{
    internal class Options
    {
        public Options(string targetFolderPath,
                       string projectName,
                       //bool addAuthenticator,
                       IList<string> viewNameList)
        {
            if (string.IsNullOrWhiteSpace(targetFolderPath))
            {
                throw new ArgumentException($"'{nameof(targetFolderPath)}' cannot be null or whitespace.", nameof(targetFolderPath));
            }

            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException($"'{nameof(projectName)}' cannot be null or whitespace.", nameof(projectName));
            }

            if (viewNameList is null)
            {
                throw new ArgumentNullException(nameof(viewNameList));
            }

            TargetFolderPath = targetFolderPath.TrimEnd('\\', '/') + Path.DirectorySeparatorChar;

            var invalidChars = Path.GetInvalidFileNameChars();

            ProjectName = new string(projectName.Replace(' ', '_').TrimEnd('.').Replace('.','_')
                          .Where(x => !invalidChars.Contains(x))
                          .ToArray());

            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                throw new Exception($"Invalid project name supplied.");
            }

            ProjectFolderPath = $"{TargetFolderPath}{ProjectName}{Path.DirectorySeparatorChar}";
            ASPFilesFolderPath = $"{ProjectFolderPath}asp_files{Path.DirectorySeparatorChar}";
            VSCodeFolderPath = $"{ProjectFolderPath}.vscode{Path.DirectorySeparatorChar}";


            IEnumerable<ViewOption> distinctViewOptions = viewNameList
                                                            .Where(viewName => !viewName.ToLower().EndsWith("api"))
                                                            .Select(viewName => new ViewOption(viewName))
                                                            .DistinctBy(x => x.ViewName);
            ViewOptions = new List<ViewOption>();

            AddAuthenticator = false;

            foreach (var viewOption in distinctViewOptions)
            {
                if (String.Equals(viewOption.ViewName, "Login", StringComparison.OrdinalIgnoreCase))
                {
                    AddAuthenticator = true;
                }
                else
                {
                    TextHelper.CheckValidViewName("View", viewOption.ViewName, 3);
                    ViewOptions.Add(viewOption);
                }
            }

            if (ViewOptions.Count == 0)
            {
                ViewOptions.Add(new ViewOption("Default"));
            }

            IEnumerable<ViewOption> distinctApiOptions = viewNameList
                                                            .Where(viewName => viewName.ToLower().EndsWith("api"))
                                                            .Select(viewName => new ViewOption(viewName))
                                                            .DistinctBy(x => x.ViewName);

            ApiOptions = new List<ViewOption>();

            foreach (var viewOption in distinctApiOptions)
            {
                TextHelper.CheckValidViewName("Api view", viewOption.ViewName, 5);
                ApiOptions.Add(viewOption);
            }

        }

        public string TargetFolderPath { get; }

        public string ProjectName { get; }

        public string ProjectFolderPath { get; }

        public string ASPFilesFolderPath { get;}

        public string VSCodeFolderPath { get; }

        public bool AddAuthenticator { get; }

        public IList<ViewOption> ViewOptions { get; }

        public IList<ViewOption> ApiOptions { get; }
    }

    internal class ViewOption
    {
        public ViewOption(string viewName) //: this (viewName, false)
        {
            ViewName = TextHelper.StartWithUCase(viewName);
            ViewNameCamel = TextHelper.StartWithLCase(viewName);
            ViewNameNoApiSuffix = TextHelper.StripSuffix(viewName, "api");
        }

        //public ViewOption(string viewName, bool hasModelBinding)
        //{
        //    ViewName = TextHelper.StartWithUCase(viewName);
        //    ViewNameCamel = TextHelper.StartWithLCase(ViewName);
        //    HasModelBinding = hasModelBinding;
        //}

        public string ViewName { get; }

        public string ViewNameCamel { get; }

        public string ViewNameNoApiSuffix { get; }
        //public bool HasModelBinding { get; }
    }
}
