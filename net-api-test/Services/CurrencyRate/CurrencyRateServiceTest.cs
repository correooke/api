using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using net_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_api.Services.Tests
{
    [TestClass()]
    public class CurrencyRateServiceTest
    {
        [TestMethod()]
        public async Task GetRateByCurrencyCodeTest()
        {
            var cache = new Mock<ICache>();
            var dic = new Dictionary<string, double>();
            dic.Add("US", 10);

            cache
                .Setup(c => c.GetAsync<Dictionary<string, double>>(
                    It.IsAny<string>(), 
                    It.IsAny<Func<string, Task<Dictionary<string, double>>>>(), 
                    It.IsAny<TimeSpan>()))
                .ReturnsAsync(dic);
            var reqCreator = new Mock<IAPIRequestCreator>();

            var service = new CurrencyRateService(cache.Object, reqCreator.Object);
            var result = await service.GetRateByCurrencyCode("US");

            Assert.AreEqual(10, result);

        }
    }
}