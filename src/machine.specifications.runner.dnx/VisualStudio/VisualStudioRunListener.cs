using System;
using Microsoft.Dnx.Testing.Abstractions;

namespace Machine.Specifications.Runner.Dnx.VisualStudio
{
    public class VisualStudioRunListener : ISpecificationRunListener
    {
        private readonly ITestExecutionSink sink;
        private ContextInfo currentContext;
        private readonly VisualStudioTestConverter converter;

        public VisualStudioRunListener(IServiceProvider services)
        {
            sink = (ITestExecutionSink)services.GetService(typeof(ITestExecutionSink));
            converter = new VisualStudioTestConverter();
        }

        public void OnAssemblyStart(AssemblyInfo assembly)
        {
        }

        public void OnAssemblyEnd(AssemblyInfo assembly)
        {
        }

        public void OnRunStart()
        {
        }

        public void OnRunEnd()
        {
        }

        public void OnContextStart(ContextInfo context)
        {
            currentContext = context;
        }

        public void OnContextEnd(ContextInfo context)
        {
            currentContext = null;
        }

        public void OnSpecificationStart(SpecificationInfo specification)
        {
            if (currentContext == null) return;
            sink?.RecordStart(converter.GetVisualStudioTest(currentContext, specification));
        }

        public void OnSpecificationEnd(SpecificationInfo specification, Result result)
        {
            if (currentContext == null) return;
            sink?.RecordResult(converter.GetVisualStudioTestResult(currentContext, specification, result));
        }

        public void OnFatalError(ExceptionResult exception)
        {
        }
    }
}