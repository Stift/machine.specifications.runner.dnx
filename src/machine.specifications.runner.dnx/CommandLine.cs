using System.Collections.Generic;
using System.Globalization;

namespace Machine.Specifications.Runner.Dnx
{
    public class CommandLine
    {
        List<string> tests = new List<string>(); 
        public CommandLine(string[] args)
        {
            ParseArgs(args);
        }

        void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                switch (arg.ToLower(CultureInfo.InvariantCulture))
                {
                    case "--designtime":
                        DesignTime = true;
                        break;
                    case "--test":
                        if (i + 1 < args.Length)
                        {
                            i++;
                            tests.Add(args[i]);
                        }
                        break;
                }
            }
        }

        public bool DesignTime { get; private set; }

        public IEnumerable<string> Tests => tests;
    }
}