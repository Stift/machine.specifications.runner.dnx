using System;

namespace Machine.Specifications.Runner.Dnx.Output
{
    internal class OutputRunListener : ISpecificationRunListener
    {
        private readonly IOutput write;

        public OutputRunListener(IOutput write)
        {
            this.write = write;
        }

        public void OnAssemblyStart(AssemblyInfo assembly)
        {
            write.AssemblyStart(assembly);
        }

        public void OnAssemblyEnd(AssemblyInfo assembly)
        {
            write.AssemblyStart(assembly);
        }

        public void OnRunStart()
        {
            write.RunStart();
        }

        public void OnRunEnd()
        {
            write.RunEnd();
        }

        public void OnContextStart(ContextInfo context)
        {
            write.ContextStart(context);
        }

        public void OnContextEnd(ContextInfo context)
        {
            write.ContextEnd(context);
        }

        public void OnSpecificationStart(SpecificationInfo specification)
        {
            write.SpecificationStart(specification);
        }

        public void OnSpecificationEnd(SpecificationInfo specification, Result result)
        {
            switch (result.Status)
            {
                case Status.Failing:
                    write.Failed(specification,result);
                    break;
                case Status.Passing:
                    write.Passing(specification);
                    break;
                case Status.NotImplemented:
                    write.NotImplemented(specification);
                    break;
                case Status.Ignored:
                    write.Ignored(specification);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnFatalError(ExceptionResult exception)
        {
            write.FatalError(exception);
        }
    }
}