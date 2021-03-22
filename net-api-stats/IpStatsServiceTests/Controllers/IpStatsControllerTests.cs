using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpStatsService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using IpStatsService.Services;
using net_api.Infraestructure;
using IpStatsService.Domain;

namespace IpStatsService.Controllers.Tests
{
    [TestClass()]
    public class IpStatsControllerTests
    {
        class CacheForTest : ICache
        {
            public Task<T> GetAsync<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
            {
                return GetterAction(Key);
            }

            public Task<string> GetAsyncFast<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var logger = new Mock<ILogger<IpStatsController>>();
            var ipStats = new Mock<IIpReportService>();
            var cache = new CacheForTest();

            ipStats.Setup(i => i.GetIpCallsStats()).ReturnsAsync(GetIpCallsStats());
            var controller = new IpStatsController(logger.Object, ipStats.Object, cache);

            var res = await controller.Get();

            Assert.AreEqual(2, res.ByCountry.Count());
        }

        [TestMethod()]
        public async Task GetMathResultTest()
        {
            var logger = new Mock<ILogger<IpStatsController>>();
            var ipStats = new Mock<IIpReportService>();
            var cache = new CacheForTest();

            ipStats.Setup(i => i.GetIpCallsStats()).ReturnsAsync(GetIpCallsStats());
            var controller = new IpStatsController(logger.Object, ipStats.Object, cache);

            var res = await controller.Get();

            Assert.AreEqual(10283, res.MaxDistance);
            Assert.AreEqual(2824, res.MinDistance);
            Assert.AreEqual(((10283 * 5)+(2824 * 10))/15, res.AverageDistance);
        }

        private IpCallsStats GetIpCallsStats()
        {
            var ipCallsStats = new IpCallsStats()
            {
                CallsByCountryInfo = new List<CallsByCountry>()
                {
                    new CallsByCountry()
                    {
                        Calls = 5,
                        CountryCode = "ES",
                        CountryName = "España",
                        Latitude = 40,
                        Longitude = -4,
                    },
                    new CallsByCountry()
                    {
                        Calls = 10,
                        CountryCode = "BR",
                        CountryName = "Brasil",
                        Latitude = -10,
                        Longitude = -55,
                    }
                }
            };

            return ipCallsStats;
        }
    }
}