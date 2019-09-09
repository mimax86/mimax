using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mimax.Api.Services;

namespace Mimax.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocationController : ControllerBase
    {
        private readonly IGeolocationService _geolocationService;

        public GeolocationController(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
        }
        
        [HttpGet("{id}")]
        public async Task<Geolocation> GetGeolocation(Guid id)
        {
            return await _geolocationService.GetGeolocationAsync(id);
        }
    }
}