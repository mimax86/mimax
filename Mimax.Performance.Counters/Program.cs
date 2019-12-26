using System;
using System.Diagnostics;
using System.Threading;

namespace Mimax.Performance.Counters
{
    public static class Performance
    {
        public const string Category = "Mimax";

        public static void CreateCategory()
        {
            if (PerformanceCounterCategory.Exists(Category))
                PerformanceCounterCategory.Delete(Category);

            var counters = new CounterCreationDataCollection();
            var data = new CounterCreationData(
                "# Test counter", "Test counter.",
                PerformanceCounterType.NumberOfItems32);
            counters.Add(data);
            PerformanceCounterCategory.Create(
                Category, "Test counter for Mimax, Inc.",
                PerformanceCounterCategoryType.SingleInstance, counters);
        }

        private static Timer _updateTimer;

        public static void StartUpdatingCounters(Func<int> getCounter)
        {
            var counter = new PerformanceCounter(
                Category, "# Test counter", readOnly: false);
            _updateTimer = new Timer(_ => { counter.RawValue = getCounter(); }, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var counter = 0;
            Performance.CreateCategory();
            Performance.StartUpdatingCounters(() => counter++);
            Console.ReadLine();
        }
    }
}