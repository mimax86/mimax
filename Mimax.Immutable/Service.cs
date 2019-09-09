using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Mimax.Immutable
{
    public class Service
    {
        public void TestImmutable()
        {
            var stack = ImmutableStack<int>.Empty;
            stack = stack.Push(13);
            stack = stack.Push(7);
            // Displays "7" followed by "13".
            foreach (var item in stack)
                Trace.WriteLine(item);
            int lastItem;
            stack = stack.Pop(out lastItem);
            // lastItem == 7

            var builder = ImmutableList.CreateBuilder<int>();
            var list = builder.ToImmutable();
        }

        public async Task TestDataFlow()
        {
            var buffer = new BufferBlock<int>();
            var available = await buffer.OutputAvailableAsync();

        }
    }
}