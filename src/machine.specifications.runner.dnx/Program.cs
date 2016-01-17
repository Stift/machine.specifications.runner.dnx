using System;
using System.Linq;
using Machine.Specifications.Runner.Dnx.Output;
using Machine.Specifications.Runner.Dnx.VisualStudio;
using Machine.Specifications.Runner.Impl;
using Microsoft.Extensions.PlatformAbstractions;

namespace Machine.Specifications.Runner.Dnx
{
    public class Program
    {
        private readonly IServiceProvider services;
        private readonly ILibraryManager libraryManager;
        private readonly IAssemblyLoadContext loadContext;

        public Program(IServiceProvider services)
        {
            this.services = services;
            libraryManager = PlatformServices.Default.LibraryManager;
            loadContext = PlatformServices.Default.AssemblyLoadContextAccessor.Default;
        }

        [STAThread]
        public void Main(string[] args)
        {
            Console.WriteLine("Foobar");
            Run(args);
        }

        private void Run(string[] args)
        {
            var designTime = args.Any(a => a.Contains("designtime"));
            var scanner = new VisualStudioAssemblyScanner(services);
            var assemblyNames = libraryManager.GetReferencingLibraries("machine.specifications").SelectMany(l => l.Assemblies);
            foreach (var assemblyName in assemblyNames)
            {
                var assembly = loadContext.Load(assemblyName);
                if (designTime)
                {
                    scanner.SendToVisualStudio(assembly);
                }
                var listener = new AggregateRunListener(new ISpecificationRunListener[]
                    {
                        new OutputRunListener(new ColorOutput(new VerboseOutput(new DefaultConsole()))),
                        new VisualStudioRunListener(services)
                    });
                var runner = new DefaultRunner(listener, RunOptions.Default);
                runner.RunAssembly(assembly);
            }
        }
    }
}