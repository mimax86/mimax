using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Mimax.Api.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly HttpClient _httpClient;

        private readonly Subject<GeolocationTask> _queue;

        public GeolocationService()
        {
            _httpClient = new HttpClient();
            _queue = new Subject<GeolocationTask>();
            _queue.ObserveOn(Scheduler.Default)
                .GroupBy(job => job.Id)
                .SelectMany(group => group.Buffer(TimeSpan.FromMilliseconds(100)))
                .Where(tasks => tasks.Any())
                .Select(tasks => new GeolocationTasksGroup(tasks.First().Id, tasks))
                .Subscribe(OnGeolocationRequest);
            RequestInfos = new List<int>();
        }

        public List<int> RequestInfos { get; }

        public async Task<Geolocation> GetGeolocationAsync(Guid id)
        {
            var taskCompletionSource = new TaskCompletionSource<Geolocation>();
            _queue.OnNext(new GeolocationTask(id, taskCompletionSource));
            return await taskCompletionSource.Task;
        }

        private void OnGeolocationRequest(GeolocationTasksGroup tasksGroup)
        {
            try
            {
                RequestInfos.Add(tasksGroup.Tasks.Count());
                var rnd = new Random();
                foreach (var task in tasksGroup.Tasks)
                {
                    task.TaskCompletionSource.SetResult(
                        new Geolocation(task.Id,
                            task.CallingThreadId,
                            Thread.CurrentThread.ManagedThreadId,
                            rnd.NextDouble(), rnd.NextDouble()));
                }
            }
            catch (Exception e)
            {
                foreach (var task in tasksGroup.Tasks)
                {
                    task.TaskCompletionSource.SetException(e);
                }
            }
        }
    }
}