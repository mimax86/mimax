using System;
using System.Data.Odbc;

namespace Mimax.Math
{
    public struct Fraction : IComparable<Fraction>, IEquatable<Fraction>
    {
        public static Fraction Invalid = new Fraction(0, 0);

        public static Fraction Zero = Create(0);

        public static Fraction Create(int n)
        {
            return new Fraction(n, 1);
        }

        public static Fraction Create(int n, int d)
        {
            return d == 0 ? Invalid : new Fraction(n, d).Simplify();
        }

        private Fraction(int n, int d)
        {
            N = n;
            D = d;
        }

        public int N { get; }

        public int D { get; }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            return Operation(f1, f2,
                () => new Fraction(f1.N * f2.D + f2.N * f1.D, f1.D * f2.D));
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return Operation(f1, f2,
                () => new Fraction(f1.N * f2.D - f2.N * f1.D, f1.D * f2.D));
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return Operation(f1, f2,
                () => new Fraction(f1.N * f2.N, f1.D * f2.D));
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return Operation(f1, f2,
                () =>
                {
                    var d = f1.D * f2.N;
                    return d == 0 ? Invalid : new Fraction(f1.N * f2.D, d);
                });
        }

        private static Fraction Operation(Fraction f1, Fraction f2, Func<Fraction> func)
        {
            if (f1.Equals(Invalid) || f2.Equals(Invalid))
                return Invalid;
            return func().Simplify();
        }

        private Fraction Simplify()
        {
            if (Equals(Invalid))
                return Invalid;
            if (Equals(Zero))
                return Zero;
            var c = GreatestCommonDivisorIterative(N, D);
            return c >= 1 ? new Fraction(N / c, D / c) : this;
        }

        public static int GreatestCommonDivisor(int n, int d)
        {
            if (d == 0) return n;
            return GreatestCommonDivisor(d, n % d);
        }

        public static int GreatestCommonDivisorIterative(int n, int d)
        {
            while (true)
            {
                if (d == 0) return n;
                var n1 = n;
                n = d;
                d = n1 % d;
            }
        }

        public override string ToString()
        {
            return $"{N}/{D}";
        }

        public int CompareTo(Fraction other)
        {
            var nComparison = N.CompareTo(other.N);
            if (nComparison != 0) return nComparison;
            return D.CompareTo(other.D);
        }

        public bool Equals(Fraction other)
        {
            return N == other.N && D == other.D;
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (N * 397) ^ D;
            }
        }
    }
}