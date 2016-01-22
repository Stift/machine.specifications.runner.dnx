using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications.Explorers;
using Machine.Specifications.Model;
using Microsoft.Dnx.Testing.Abstractions;

namespace Machine.Specifications.Runner.Dnx.VisualStudio
{
    public class VisualStudioAssemblyScanner
    {
        private readonly ISourceInformationProvider provider;
        private readonly ITestDiscoverySink sink;
        private readonly VisualStudioTestConverter converter;

        public VisualStudioAssemblyScanner(IServiceProvider services)
        {
            provider = (ISourceInformationProvider) services.GetService(typeof (ISourceInformationProvider));
            sink = (ITestDiscoverySink) services.GetService(typeof (ITestDiscoverySink));
            converter = new VisualStudioTestConverter();
        }

        public void SendToVisualStudio(Assembly assembly)
        {
            if (sink == null)
            {
                return;
            }
            var explorer = new AssemblyExplorer();
            var contexts = explorer.FindContextsIn(assembly);
            foreach (var context in contexts)
            {
                foreach (var test in ConvertToVisualStudioTests(context))
                {
                    sink?.SendTest(test);
                }
            }
        }

        public IEnumerable<Context> GetContexts(Assembly assembly)
        {
            var explorer = new AssemblyExplorer();
            var contexts = explorer.FindContextsIn(assembly);
            return contexts;
        }
        private IEnumerable<Test> ConvertToVisualStudioTests(Context context)
        {
            return context.Specifications.Select(specification => ConvertToVisualStudioTest(context, specification));
        }

        private Test ConvertToVisualStudioTest(Context context, Specification specification)
        {
            var test = converter.GetVisualStudioTest(context, specification);
            if (context.Instance == null)
            {
                return test;
            }
            var it = (Delegate) specification.FieldInfo.GetValue(context.Instance);
            if (it == null)
            {
                return test;
            }
            var methodInfo = it.GetMethodInfo();
            var sourceInfo = provider.GetSourceInformation(methodInfo);
            test.CodeFilePath = sourceInfo.Filename;
            test.LineNumber = sourceInfo.LineNumber;
            return test;
        }
    }
}