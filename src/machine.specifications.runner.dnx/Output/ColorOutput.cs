using System;

namespace Machine.Specifications.Runner.Dnx.Output
{
    public class ColorOutput : IOutput
    {
        readonly IOutput inner;

        public ColorOutput(IOutput inner)
        {
            this.inner = inner;
        }

        public void RunStart()
        {
            inner.RunStart();
        }

        public void RunEnd()
        {
            inner.RunEnd();
        }

        public void AssemblyStart(AssemblyInfo assembly)
        {
            inner.AssemblyStart(assembly);
        }

        public void AssemblyEnd(AssemblyInfo assembly)
        {
            inner.AssemblyEnd(assembly);
        }

        public void ContextStart(ContextInfo context)
        {
            inner.ContextStart(context);
        }

        public void ContextEnd(ContextInfo context)
        {
            inner.ContextEnd(context);
        }

        public void SpecificationStart(SpecificationInfo specification)
        {
        }

        public void Passing(SpecificationInfo specification)
        {
            Color(ConsoleColor.Green, () =>
            {
                inner.SpecificationStart(specification);
                inner.Passing(specification);
            });
        }

        public void NotImplemented(SpecificationInfo specification)
        {
            Color(ConsoleColor.Gray, () =>
            {
                inner.SpecificationStart(specification);
                inner.NotImplemented(specification);
            });
        }

        public void Ignored(SpecificationInfo specification)
        {
            Color(ConsoleColor.Yellow, () =>
            {
                inner.SpecificationStart(specification);
                inner.Ignored(specification);
            });
        }

        public void Failed(SpecificationInfo specification, Result result)
        {
            Color(ConsoleColor.Red, () =>
            {
                inner.SpecificationStart(specification);
                inner.Failed(specification, result);
            });
        }

        public void FatalError(ExceptionResult exception)
        {
            Color(ConsoleColor.Red, () =>
            {
                inner.FatalError(exception);
            });
        }

        static void Color(ConsoleColor color, Action action)
        {
            try
            {
                Console.ForegroundColor = color;
                action();
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}