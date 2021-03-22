using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using net_api.Controllers.Models;
using net_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using GeoCoordinatePortable;
using System;
using net_api.Domain;
using net_api.Services.UpdateStats;

namespace net_api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class IpInfoController : ControllerBase
    {
        private readonly IIpInfoService _service;
        private readonly IUpdateStatsService _stats;
        private readonly ILogger<IpInfoController> _logger;

        public IpInfoController(ILogger<IpInfoController> logger, IIpInfoService service, IUpdateStatsService stats)
        {
            _logger = logger;
            _service = service;
            _stats = stats;
        }

        [HttpGet]
        public async Task<IpInfo> Get(
            [Required]
            [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
            ErrorMessage = "The supplied parameter it is not a valid IPv4")] string ip)
        {
            try
            {
                _logger.LogInformation($"IpInfo called with ip {ip}");

                var info = await _service.GetInfoByIp(ip);
                var from = new GeoCoordinate(-34, -64);

                _stats.RegisterVisitor(ip, info.Alpha2Code);
                return CreateIpInfo(ip, info, from);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                throw;
            }
        }

        private IpInfo CreateIpInfo(string ip, Country info, GeoCoordinate from)
        {
            Validate(info);
            var to = new GeoCoordinate(info.Coordinate.Latitude, info.Coordinate.Longitude);
            Func<Domain.Currency, IpInfoCurrency> selector = c => (new IpInfoCurrency()
            {
                Name = c.Name,
                Symbol = c.Symbol,
                Rate = c.Rate
            });
            
            return new IpInfo()
            {
                IP = ip,
                CurrentDate = DateTime.Now,
                CountryName = info.Name,
                CountryCode = info.Alpha2Code,
                Currencies = info.Currencies?.Select(selector),
                Distance = Convert.ToInt32(from.GetDistanceTo(to)),
                Languages = info.Languages?.Select(l => l.Name),
                Latitude = info.Coordinate.Latitude,
                Longitude = info.Coordinate.Longitude,
                TimeZones = info.TimeZones?.Select(t => t)
            };
        }

        private void Validate(Country info)
        {
            if (info.Coordinate == null)
                throw new Exception("Country data must contain coordinate info");
        }
    } 
}
