using FluentAssertions;
using NUnit.Framework;

namespace Mimax.Susquehanna.Test
{
    public class StackMachineTest
    {
        [Test]
        public void BasicTest()
        {
            var result = StackMachine.Solution("13 DUP 4 POP 5 DUP + DUP + -");
            result.Should().Be(7);

            result = StackMachine.Solution("DUP DUP");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("14");
            result.Should().Be(14);

            result = StackMachine.Solution("14 DUP");
            result.Should().Be(14);

            result = StackMachine.Solution("14 POP");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("14 6 +");
            result.Should().Be(20);

            result = StackMachine.Solution("4 14 -");
            result.Should().Be(10);

            result = StackMachine.Solution("POP");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("DUP");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("14 4 5 + +");
            result.Should().Be(23);

            result = StackMachine.Solution("1040000");
            result.Should().Be(1040000);

            result = StackMachine.Solution("1050000");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("1040000 1000 +");
            result.Should().Be(1041000);

            result = StackMachine.Solution("1040000 10000 +");
            result.Should().Be(StackMachine.ErrorValue);

            result = StackMachine.Solution("1041000 1040000 -");
            result.Should().Be(StackMachine.ErrorValue);
        }
    }
}