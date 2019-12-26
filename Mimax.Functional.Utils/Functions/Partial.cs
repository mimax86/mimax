using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mimax.Functional.Utils.Functions
{
    public static class Partial
    {
        public static Func<int, Func<int, int>> ReminderF => denominator => nominator => nominator % denominator;

        public static Func<int, int, int> ReminderN => (nominator, denominator) => nominator % denominator;

        public static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> f, T2 t2)
            => (T1 t1) => f(t1, t2);

        public static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1, T2, T3, R> f, T3 t3)
            => (T1 t1, T2 t2) => f(t1, t2, t3);

        public static IEnumerable<T> Map<T>(this IEnumerable<T> sequence, Func<T, T> func)
            => sequence.Aggregate(ImmutableList<T>.Empty, (acc, t) => acc.Add(func(t)));

        public static IEnumerable<T> Where<T>(this IEnumerable<T> sequence, Func<T, bool> func)
            => sequence.Aggregate(ImmutableList<T>.Empty, (acc, t) => func(t) ? acc.Add(t) : acc);

        public static IEnumerable<T> Bind<T>(this IEnumerable<T> sequence, Func<T, IEnumerable<T>> func)
            => sequence.Aggregate(ImmutableList<T>.Empty, (acc, t) => acc.AddRange(func(t)));
    }
}