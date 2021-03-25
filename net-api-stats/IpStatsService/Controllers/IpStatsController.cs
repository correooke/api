using GeoCoordinatePortable;
using IpStatsService.Controllers.Models;
using IpStatsService.Domain;
using IpStatsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using net_api.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpStatsController : ControllerBase
    {
        private readonly ILogger<IpStatsController> _logger;
        private readonly ICache _cache;
        private readonly IIpReportService _ipStatsService;

        public IpStatsController(ILogger<IpStatsController> logger, IIpReportService ipStatsService, ICache cache)
        {
            _logger = logger;
            _ipStatsService = ipStatsService;
            _cache = cache;
        }

        /* 
            Endpoint version for efficiency purposes
         * [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cache.GetAsyncFast("", (key) => GetUpdatedStat(), TimeSpan.FromSeconds(10)));
        }*/

        [HttpGet]
        public async Task<StatData> Get()
        {
            return await _cache.GetAsync("", (key) => GetUpdatedStat(), TimeSpan.FromSeconds(2));
        }


        private async Task<StatData> GetUpdatedStat()
        {
            var ipCallsStats = await _ipStatsService.GetIpCallsStats();
            GeoCoordinate From = new GeoCoordinate(-34, -64);

            var data = new StatData()
            {
                LastUpdate = DateTime.Now,
                ByCountry = ipCallsStats.CallsByCountryInfo
                .Where(c => c.Calls > 0)
                .Select(c => new StatLine()
                {
                    Country = c.CountryName,
                    Distance = c.GetDistance(From),
                    Calls = c.Calls,
                }),
            };
            return data;
        }
    }
}
