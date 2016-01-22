using System;
using System.Security.Cryptography;
using System.Text;
using Machine.Specifications.Model;
using Machine.Specifications.Runner.Impl;
using Microsoft.Dnx.Testing.Abstractions;

namespace Machine.Specifications.Runner.Dnx.VisualStudio
{
    public class VisualStudioTestConverter
    {
        private SHA1 hashing;

        public VisualStudioTestConverter()
        {
            hashing = SHA1.Create();
        }

        public Test GetVisualStudioTest(Context context, Specification specification)
        {
            return GetVisualStudioTest(context.GetInfo(), specification.GetInfo());
        }

        public Test GetVisualStudioTest(ContextInfo context, SpecificationInfo specification)
        {
            var test = new Test();
            if (string.IsNullOrWhiteSpace(context.Concern))
            {
                // no function yet, because the Dnx Testing host has no support https://github.com/aspnet/Tooling/issues/182
                test.Properties["Category"] = context.Concern;
            }
            test.FullyQualifiedName =
                $"{context.AssemblyName}:{context.TypeName}.{specification.Leader} {specification.FieldName}";
            test.DisplayName = $"{context.Name} {specification.Leader} {specification.Name}";
            test.Id = GuidFromString(test.FullyQualifiedName);
            return test;
        }

        private Guid? GuidFromString(string fullyQualifiedName)
        {
            var hashBytes = hashing.ComputeHash(Encoding.UTF8.GetBytes(fullyQualifiedName));
            Array.Resize(ref hashBytes, 16);
            return new Guid(hashBytes);
        }

        public TestResult GetVisualStudioTestResult(ContextInfo context, SpecificationInfo specification, Result result)
        {
            var vsResult = new TestResult(GetVisualStudioTest(context, specification));
            switch (result.Status)
            {
                case Status.Failing:
                    vsResult.Outcome = TestOutcome.Failed;
                    vsResult.ErrorMessage = result.Exception.Message;
                    vsResult.ErrorStackTrace = result.Exception.StackTrace;
                    break;
                case Status.Passing:
                    vsResult.Outcome = TestOutcome.Passed;
                    break;
                case Status.NotImplemented:
                    vsResult.Outcome = TestOutcome.NotFound;
                    break;
                case Status.Ignored:
                    vsResult.Outcome = TestOutcome.Skipped;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            vsResult.Messages.Add(context.CapturedOutput);
            vsResult.Messages.Add(specification.CapturedOutput);
            return vsResult;
        }
    }
}