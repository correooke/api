using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using net_api.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using net_api.Services;
using System;
using System.Threading.Tasks;

namespace net_api_test
{
    [TestClass]
    public class CountryServiceTest
    {
        public class CacheTest : ICache
        {
            public Object Result { get; set; }

            public Task<T> GetAsync<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
            {
                return Task.FromResult((T)Result);
            }

            public Task<string> GetAsyncFast<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public async Task GetByCodeTest()
        {
            var country = new Country()
            {
                Alpha2Code = "US",
                Name = "United States of America"
            };
            var cache = new CacheTest();
            cache.Result = country;
            var reqCreator = new Mock<IAPIRequestCreator>();

            var service = new CountryService(cache, reqCreator.Object);
            var countryToTest = await service.GetByCode("US");

            Assert.AreEqual(country.Name, countryToTest.Name);
            Assert.AreEqual(country.Alpha2Code, countryToTest.Alpha2Code);
        }
    }
}
