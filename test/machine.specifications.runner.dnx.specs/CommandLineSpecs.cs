using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using Machine.Specifications.Runner.Dnx;

namespace machine.specifications.runner.dnx.specs
{
    public class WennHavingDesignTimeAndTestInArgs
    {
        static CommandLine commandLine;
        Establish context = () =>
        {
            var args = new[]
            {
                "--test", "foobar",
                "--test", "hello",
                "--designtime",
                "--test", "again"
            };
            commandLine = new CommandLine(args);
        };

        It should_has_design_time_true = () => commandLine.DesignTime.Should().BeTrue();
        It should_has_three_tests = () => commandLine.Tests.Count().Should().Be(3);
        It should_conaint_the_test = () => commandLine.Tests.Should().Contain("foobar", "hello", "again");
    }

    public class WennHavingNoDesignTimeAndTestInArgs
    {
        static CommandLine commandLine;
        Establish context = () =>
        {
            var args = new[]
            {
                "--test", "hello",
                "--test", "again"
            };
            commandLine = new CommandLine(args);
        };

        It should_has_design_time_true = () => commandLine.DesignTime.Should().BeFalse();
        It should_has_two_tests = () => commandLine.Tests.Count().Should().Be(2);
        It should_conaint_the_test = () => commandLine.Tests.Should().Contain("hello", "again");
    }

    public class WennHavingDesignTimeAndNoTestInArgs
    {
        static CommandLine commandLine;
        Establish context = () =>
        {
            var args = new[]
            {
                "--designtime"
            };
            commandLine = new CommandLine(args);
        };

        It should_has_design_time_true = () => commandLine.DesignTime.Should().BeTrue();

        It should_has_no_tests = () => commandLine.Tests.Should().BeEmpty();

    }

}