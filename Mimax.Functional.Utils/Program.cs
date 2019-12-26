using System;
using static Mimax.Functional.Utils.Functions.Partial;

namespace Mimax.Functional.Utils
{
    class Program
    {
        static void Main(string[] args)
        {
            var reminder5 = ReminderF(5);
            var test1 = reminder5(10);
            var test2 = reminder5(-10);
            var test3 = reminder5(6);

            reminder5 = ReminderN.ApplyR(5);
            test1 = reminder5(10);
            test2 = reminder5(-10);
            test3 = reminder5(6);
        }
    }
}
