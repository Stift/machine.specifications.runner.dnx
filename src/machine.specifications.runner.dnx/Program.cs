using System;
using System.Linq;
using Machine.Specifications.Annotations;
using Machine.Specifications.Runner.Dnx.Output;
using Machine.Specifications.Runner.Dnx.VisualStudio;
using Machine.Specifications.Runner.Impl;
using Microsoft.Extensions.PlatformAbstractions;

namespace Machine.Specifications.Runner.Dnx
{
    [UsedImplicitly]
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
            var commandLine = new CommandLine(args);
            var scanner = new VisualStudioAssemblyScanner(services);
            var assemblyNames =
                libraryManager.GetReferencingLibraries("machine.specifications").SelectMany(l => l.Assemblies);
            foreach (var assemblyName in assemblyNames)
            {
                var assembly = loadContext.Load(assemblyName);
                if (commandLine.DesignTime)
                {
                    scanner.SendToVisualStudio(assembly);
                    var listener = new AggregateRunListener(new ISpecificationRunListener[]
                    {
                        new VisualStudioRunListener(services)
                    });
                    var runner = new DefaultRunner(listener, RunOptions.Default);
                    if (commandLine.Tests.Any())
                    {
                        var converter = new VisualStudioTestConverter();
                        var contexts = scanner.GetContexts(assembly);
                        foreach (var context in contexts)
                        {
                            foreach (var specification in context.Specifications)
                            {
                                var test = converter.GetVisualStudioTest(context, specification);
                                if (commandLine.Tests.Any(t => t == test.FullyQualifiedName))
                                {
                                    runner.RunMember(assembly, specification.FieldInfo);
                                }
                            }
                        }
                    }
                    else
                    {
                        runner.RunAssembly(assembly);
                    }
                }
                else
                {
                    var listener = new AggregateRunListener(new ISpecificationRunListener[]
                    {
                        new OutputRunListener(new ColorOutput(new VerboseOutput(new DefaultConsole()))),
                    });
                    var runner = new DefaultRunner(listener, RunOptions.Default);
                    runner.RunAssembly(assembly);
                }
            }
        }
    }
}