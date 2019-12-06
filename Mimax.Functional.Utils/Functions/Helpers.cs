using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimax.Functional.Utils.Functions
{
    public static class Helpers
    {
        public static ISet<R> Map<T, R>(this ISet<T> set, Func<T, R> func) =>
            set.Select(func).ToHashSet();
    }
}
