using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Model;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Dnx.VisualStudio;
using Microsoft.Dnx.Testing.Abstractions;

namespace machine.specifications.runner.dnx.specs.VisualStudio
{
    [Subject(typeof (VisualStudioTestConverter))]
    public class VisualStudioTestConverterSpecs : WithSubject<VisualStudioTestConverter>
    {
        private Establish context =
            () =>
            {
                testContext = new ContextInfo("wenn", "foo", "theType", "namepace", "assembly");
                testSpecification = new SpecificationInfo("it", "should do", "theType", "should_do");
            };

        private Because of = () =>
        {
            result = Subject.GetVisualStudioTest(testContext, testSpecification);
        };

        private It should_set_the_guid;//= () => result.Id.Should().HaveValue();

        private static ContextInfo testContext;
        private static SpecificationInfo testSpecification;
        private static Test result;
    }
}