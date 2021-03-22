
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using net_api.Controllers;
using net_api.Domain;
using net_api.Services;
using net_api.Services.UpdateStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_api.Controllers.Tests
{
    [TestClass()]
    public class IpInfoControllerTests
    {
        [TestMethod()]
        public async Task GetTest()
        {
            var logger = new Mock<ILogger<IpInfoController>>();
            var ipInfoService = new Mock<IIpInfoService>();
            var stats = new Mock<IUpdateStatsService>();

            var country = new Country() {
                Alpha2Code = "US",
                Coordinate = new Coordinate(),
                Currencies = new List<Currency>() { new Currency() {  Code = "USD", Name = "Dolar", Symbol = "$" } },
                Languages = new List<Language>() { new Language() { IsoCode = "EN", Name = "English" } },
                Name = "United States",
                TimeZones = new List<String>() { "UTC" },
            };

            ipInfoService.Setup(i => i.GetInfoByIp(It.IsAny<String>())).ReturnsAsync(country);
            stats.Setup(s => s.RegisterVisitor(It.IsAny<String>(), It.IsAny<String>())).Verifiable();

            var controller = new IpInfoController(logger.Object, ipInfoService.Object, stats.Object);

            var res = await controller.Get("1.2.3.4");

            Assert.AreEqual("US", res.CountryCode);
            stats.Verify();
        }
    }
}