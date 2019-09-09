using System;

namespace Mimax.Api.Services
{
    public class Geolocation
    {
        public Geolocation(Guid id, int callingThreadId, int executingThreadId, double x, double y)
        {
            Id = id;
            CallingThreadId = callingThreadId;
            ExecutingThreadId = executingThreadId;
            X = x;
            Y = y;
        }

        public Guid Id { get; }
        public int CallingThreadId { get; }
        public int ExecutingThreadId { get; }
        public double X { get; }
        public double Y { get; }
    }
}