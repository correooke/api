using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using net_api.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using net_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static net_api_test.CountryServiceTest;
using Ip2Country = net_api.Infraestructure.ExternalAPI.SerializedObjects.Ip2Country;

namespace net_api.Services.Tests
{
    [TestClass()]
    public class IpInfoServiceTests
    {
        [TestMethod()]
        public async Task GetInfoByIpTest()
        {
            var country = new Country()
            {
                Alpha2Code = "US",
                Name = "United States of America"
            };
            Mock<ICache> cache = GetCache();

            Mock<ICache> cacheRate = CacheRate();

            var cacheCountry = CacheCountry();
            var reqCreator = new Mock<IAPIRequestCreator>();

            var rateService = new CurrencyRateService(cacheRate.Object, reqCreator.Object); // 10
            var countryService = new CountryService(cacheCountry, reqCreator.Object);

            var service = new IpInfoService(cache.Object, reqCreator.Object, countryService, rateService);
            var result = await service.GetInfoByIp("1.2.3.4");

            Assert.AreEqual("US", result.Alpha2Code);
            Assert.AreEqual(10, result.Currencies.First().Rate);
        }

        private static Mock<ICache> GetCache()
        {
            var cache = new Mock<ICache>();

            var basicInfo = new Ip2Country.Root() { countryCode = "US" };

            cache.Setup(c => c.GetAsync(It.IsAny<string>(),
                It.IsAny<Func<string, Task<Ip2Country.Root>>>(),
                It.IsAny<TimeSpan>()))
                .ReturnsAsync(basicInfo);
            return cache;
        }

        private ICache CacheCountry()
        {
            var country = new Country()
            {
                Alpha2Code = "US",
                Name = "United States of America",
                Currencies = new List<Currency>() { new Currency() { Code = "USD" } }
            };
            var cache = new CacheTest();
            cache.Result = country;
            return cache;
        }

        private Mock<ICache> CacheRate()
        {
            var cacheRate = new Mock<ICache>();
            var dic = new Dictionary<string, double>();
            dic.Add("USD", 10);

            cacheRate
                .Setup(c => c.GetAsync<Dictionary<string, double>>(
                    It.IsAny<string>(),
                    It.IsAny<Func<string, Task<Dictionary<string, double>>>>(),
                    It.IsAny<TimeSpan>()))
                .ReturnsAsync(dic);
            return cacheRate;
        }
    }
}