using System;
using System.Threading.Tasks;

namespace Mimax.Reactive
{
    public static class TaskExtensions
    {
        public static Task<R> SelectMany<T, R>(this Task<T> source, Func<T, Task<R>> selector) =>
            source.ContinueWith(t => selector(t.Result)).Unwrap();
    }
}