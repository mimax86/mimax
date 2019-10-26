using NUnit.Framework;
using ReactiveAgent.Agents;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Mimax.Tests
{
    [TestFixture]
    public class ConcurrencyTests
    {
        private IAgent<string> _printer;

        [SetUp]
        public void Setup()
        {
            _printer = Agent.Start((string msg) => 
                WriteLine($"{msg} on thread {Thread.CurrentThread.ManagedThreadId}"));
        }

        [Test]
        public async Task Agent_Test()
        {
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
            await Print();
        }

        private async Task Print()
        {
            await Task.Run(async () =>
            {
                WriteLine($"Current running Thread {Thread.CurrentThread.ManagedThreadId}");
                await _printer.Send("Agent Message");
            });
        }
    }
}
