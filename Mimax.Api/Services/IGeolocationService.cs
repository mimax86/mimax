using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimax.Api.Services
{
    public interface IGeolocationService
    {
        Task<Geolocation> GetGeolocationAsync(Guid id);

        List<int> RequestInfos { get; }
    }
}