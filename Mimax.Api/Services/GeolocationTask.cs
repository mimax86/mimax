using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mimax.Api.Services
{
    public class GeolocationTask
    {
        public Guid Id { get; }

        public int CallingThreadId { get; }

        public TaskCompletionSource<Geolocation> TaskCompletionSource { get; }

        public GeolocationTask(Guid id, TaskCompletionSource<Geolocation> taskCompletionSource)
        {
            Id = id;
            TaskCompletionSource = taskCompletionSource;
            CallingThreadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}