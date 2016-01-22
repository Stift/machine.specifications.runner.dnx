namespace Machine.Specifications.Runner.Dnx.Output
{
    class VerboseOutput : IOutput
    {
        readonly IConsole console;

        public VerboseOutput(IConsole console)
        {
            this.console = console;
        }

        public void RunStart()
        {
        }

        public void RunEnd()
        {
            EmptyLine();
        }

        public void AssemblyStart(AssemblyInfo assembly)
        {
            EmptyLine();
            console.WriteLine("Specs in " + assembly.Name + ":");
        }

        public void AssemblyEnd(AssemblyInfo assembly)
        {
        }

        public void ContextStart(ContextInfo context)
        {
            EmptyLine();
            console.WriteLine(context.FullName);
        }

        public void ContextEnd(ContextInfo context)
        {
        }

        public void SpecificationStart(SpecificationInfo specification)
        {
            console.Write($"{specification.Leader}: " + specification.Name);
        }

        public void Passing(SpecificationInfo specification)
        {
            EmptyLine();
        }

        public void NotImplemented(SpecificationInfo specification)
        {
            console.WriteLine(" (NOT IMPLEMENTED)");
        }

        public void Ignored(SpecificationInfo specification)
        {
            console.WriteLine(" (IGNORED)");
        }

        public void Failed(SpecificationInfo specification, Result result)
        {
            console.WriteLine(" (FAIL)");
            console.WriteLine(result.Exception.ToString());
        }

        public void FatalError(ExceptionResult exception)
        {
//            _console.WriteLine(exception.ToString());
            EmptyLine();
        }

        void EmptyLine()
        {
            console.WriteLine("");
        }
    }
}