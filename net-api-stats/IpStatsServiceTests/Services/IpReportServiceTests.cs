using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpStatsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net_api.Infraestructure;
using Moq;
using IpStatsService.Domain;
using IpInfoCore.Infraestructure.EventRegister;
using net_api.Infraestructure.ExternalAPI;

namespace IpStatsService.Services.Tests
{
    [TestClass()]
    public class IpReportServiceTests
    {
        [TestMethod()]
        public async Task GetIpCallsStatsTest()
        {
            var eventCounter = new EventCounter() { EventType = "", Quantity = 1 };
            var register = new Mock<IEventRegister>();
            register.Setup(r => r.EventsRegisteredByType(It.IsAny<String>()))
                .Returns(eventCounter);
                

            var cache = new Mock<ICache>();
            var stats = new IpCallsStats();
            stats.CallsByCountryInfo = new List<CallsByCountry>() { new CallsByCountry() { Calls = 1 } };
            
            cache.Setup(c => c.GetAsync(
                It.IsAny<String>(),
                It.IsAny<Func<String, Task<IpCallsStats>>>(),
                null))
                .ReturnsAsync(stats);

            var reqCreator = new Mock<IAPIRequestCreator>();

            var service = new IpReportService(cache.Object, reqCreator.Object, register.Object);

            var ipCalls = await service.GetIpCallsStats();

            Assert.AreEqual(1, ipCalls.CallsByCountryInfo.First().Calls);
        }
    }
}