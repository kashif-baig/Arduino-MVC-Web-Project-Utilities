namespace Arduino.MVC.NewProj
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    Console.WriteLine($"Usage: {typeof(Program).Assembly.GetName().Name} <project name> <view name> ...");
                    return -1;
                }
                var viewNameList = new List<string>();

                for (int i=1; i < args.Length; i++)
                {
                    viewNameList.Add(args[i]);
                }

                var options = new Options(Environment.CurrentDirectory
                                        , args[0]
                                        , viewNameList
                                     );

                var generator = new TemplateGenerator(options);
                generator.Generate();

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}