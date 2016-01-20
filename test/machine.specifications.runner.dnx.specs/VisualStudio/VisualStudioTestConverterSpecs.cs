using System;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Dnx.VisualStudio;
using Microsoft.Dnx.Testing.Abstractions;

namespace machine.specifications.runner.dnx.specs.VisualStudio
{
    [Subject(typeof (VisualStudioTestConverter))]
    public class VisualStudioTestConverterSpecs : WithSubject<VisualStudioTestConverter>
    {
        static ContextInfo testContext;
        static SpecificationInfo testSpecification;
        static Test result;

        Establish context = () =>
        {
            testContext = new ContextInfo("When foo", "foo", "namespace.When_foo", "namespace", "assembly");
            testSpecification = new SpecificationInfo("it", "should do", "theType", "should_do");
        };

        Because of = () => { result = Subject.GetVisualStudioTest(testContext, testSpecification); };

        It should_has_the_guid_set = () => result.Id.Should().Be(new Guid("d3f77b77-1c7c-89f0-1fdd-451536df0faf"));

        It should_has_the_right_display_name = () => result.DisplayName.Should().Be("When foo, it should do");

        It should_has_a_fully_qualified_name = () => result.FullyQualifiedName.Should().Be("assembly:namespace.When_foo.it.should_do");

    }
}