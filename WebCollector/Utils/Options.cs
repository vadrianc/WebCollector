namespace WebCollector.Utils
{
    using System;
    using System.Text;
    using SoftwareControllerLib.Utils;

    /// <summary>
    /// Options class.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class.
        /// </summary>
        /// <param name="args">The arguments used to set the options.</param>
        public Options(params string[] args)
        {
            ParseArgs(args);
        }

        private void ParseArgs(string[] args)
        {
            for (int index = 0; index < args.Length; index++) {
                switch (args[index]) {
                case "-h":
                case "--help":
                    Help = true;
                    break;
                case "-c":
                case "--config":
                    if (index == args.Length - 1) throw new ArgumentException();
                    index++;
                    Config = args[index];
                    break;
                }
            }

            if (Help) {
                ShowHelp();
                Environment.Exit(0);
            }

            if (Config == null) throw new ArgumentException("No configuration file provided.");
        }

        private void ShowHelp()
        {
            StringBuilder helpBuilder = new StringBuilder();
            helpBuilder.Append("WebCollector is an utility for automatically navigating a website ");
            helpBuilder.Append("and collecting HTML tag values which match the given configuration.");
            helpBuilder.Append(Environment.NewLine);
            helpBuilder.Append(Environment.NewLine);
            helpBuilder.Append("The available options are:");
            helpBuilder.Append(Environment.NewLine);
            helpBuilder.Append(Environment.NewLine);
            helpBuilder.Append("-h|--help - display this help message.");
            helpBuilder.Append(Environment.NewLine);
            helpBuilder.Append("-c|--config <config_file.xml> - set the configuration file which ");
            helpBuilder.Append("contains the collection of rules and actions used to navigate and ");
            helpBuilder.Append("collect HTML tag values from a website.");

            ConsoleOutput.Instance.Message(helpBuilder.ToString());
        }

        /// <summary>
        /// The configuration file.
        /// </summary>
        public string Config
        {
            get;
            private set;
        }

        /// <summary>
        /// Flag indicating if the help message should be displayed.
        /// </summary>
        public bool Help
        {
            get;
            private set;
        }
    }
}
