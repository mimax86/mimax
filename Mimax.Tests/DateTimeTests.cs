using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace Mimax.Tests
{
    [TestFixture]
    public class DateTimeTests
    {
        [Test]
        public void Test()
        {
            var zones = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo zone in zones)
                Console.WriteLine(zone.Id);

            DaylightTime changes = TimeZone.CurrentTimeZone.GetDaylightChanges(2010);
            TimeSpan halfDelta = new TimeSpan(changes.Delta.Ticks / 2);
            DateTime utc1 = changes.End.ToUniversalTime() - halfDelta;
            DateTime utc2 = utc1 - changes.Delta;
            
            DateTime loc1 = utc1.ToLocalTime(); // (Pacific Standard Time)
            DateTime loc2 = utc2.ToLocalTime();
            Console.WriteLine(loc1); // 2/11/2010 1:30:00 AM
            Console.WriteLine(loc2); // 2/11/2010 1:30:00 AM
            Console.WriteLine(loc1 == loc2);
        }
    }
}
