using FluentAssertions;
using NUnit.Framework;

namespace Mimax.Math.Test
{
    public class FractionTests
    {
        [TestCase(1, 0, 1, 3)]
        [TestCase(1, 2, 1, 0)]
        public void Addition_Invalid_Test(int n1, int d1, int n2, int d2)
        {
            var f1 = Fraction.Create(n1, d1);
            var f2 = Fraction.Create(n2, d2);
            var result = f1 + f2;
            result.Should().Be(Fraction.Invalid);
        }

        [Test]
        public void Addition_Test()
        {
            var f1 = Fraction.Create(1, 2);
            var f2 = Fraction.Create(1, 3);
            var result = f1 + f2;
            result.Should().Be(Fraction.Create(5, 6));
        }

        [Test]
        public void Addition_Complex_Test()
        {
            var f1 = Fraction.Create(1, 3);
            var f2 = Fraction.Create(1, 3);
            var result = f1 + f2;
            result.Should().Be(Fraction.Create(2, 3));
        }

        [TestCase(1, 0, 1, 3)]
        [TestCase(1, 2, 1, 0)]
        public void Subtraction_Invalid_Test(int n1, int d1, int n2, int d2)
        {
            var f1 = Fraction.Create(n1, d1);
            var f2 = Fraction.Create(n2, d2);
            var result = f1 - f2;
            result.Should().Be(Fraction.Invalid);
        }

        [Test]
        public void Subtraction_Test()
        {
            var f1 = Fraction.Create(1, 2);
            var f2 = Fraction.Create(1, 3);
            var result = f1 - f2;
            result.Should().Be(Fraction.Create(1, 6));
        }

        [Test]
        public void Multiplication_Test()
        {
            var f1 = Fraction.Create(1, 2);
            var f2 = Fraction.Create(1, 3);
            var sum = f1 * f2;
            sum.Should().Be(Fraction.Create(1, 6));
        }

        [Test]
        public void Division_Test()
        {
            var f1 = Fraction.Create(1, 2);
            var f2 = Fraction.Create(1, 3);
            var sum = f1 / f2;
            sum.Should().Be(Fraction.Create(3, 2));
        }

        [Test]
        public void Division_Invalid_Test()
        {
            var f1 = Fraction.Create(1, 2);
            var f2 = Fraction.Zero;
            var sum = f1 / f2;
            sum.Should().Be(Fraction.Invalid);
        }

        [Test]
        public void Division_Zero_Test()
        {
            var f1 = Fraction.Zero;
            var f2 = Fraction.Create(1, 2);
            var sum = f1 / f2;
            sum.Should().Be(Fraction.Zero);
        }

        [Test]
        public void Simplify_Test()
        {
            var f = Fraction.Create(15, 20);
            f.Should().Be(Fraction.Create(3, 4));
        }

        [Test]
        public void Simplify_Complex_Test()
        {
            var f = Fraction.Create(450, 600);
            f.Should().Be(Fraction.Create(3, 4));
        }
    }
}