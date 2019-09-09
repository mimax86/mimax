using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Mimax.Api.Services;
using NUnit.Framework;

namespace Tests
{
    public class GeolocationServiceTests
    {
        private IGeolocationService _geolocationService;

        [SetUp]
        public void Setup()
        {
            _geolocationService = new GeolocationService();
        }

        [TestCase(1000, 1000)]
        public async Task Geolocation_Service_Is_Buffering_Requests(int batchesCount, int requestCount)
        {
            var tasks = Enumerable.Range(0, batchesCount)
                .SelectMany(_ =>
                {
                    var id = Guid.NewGuid();
                    return Enumerable.Range(0, requestCount).Select(__ =>
                        Task.Run(() => _geolocationService.GetGeolocationAsync(id)));
                }).ToList();
            var geolocations = await Task.WhenAll(tasks);
            var callingThreadIds = geolocations.Select(geolocation => geolocation.CallingThreadId).Distinct().ToList();
            var executingThreadIds =
                geolocations.Select(geolocation => geolocation.ExecutingThreadId).Distinct().ToList();
        }
    }
}