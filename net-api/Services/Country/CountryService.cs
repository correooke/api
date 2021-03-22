using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using net_api.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using DataCountry = net_api.Infraestructure.ExternalAPI.SerializedObjects.DataCountry;

namespace net_api.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICache _cache;
        private readonly IAPIRequestCreator _apiRequestCreator;

        public CountryService(ICache cache, IAPIRequestCreator apiRequestCreator)
        {
            _cache = cache;
            _apiRequestCreator = apiRequestCreator;
        }

        public async Task<Country> GetByCode(string code)
        {
            var country = await _cache.GetAsync<Country>(code, (key) => GetterAction(key)); // Static information
            return country;
        }

        protected async Task<Country> GetterAction(string code)
        {
            var req = _apiRequestCreator.Create(BaseUrls.countryInfoUrl);

            var apiCountry = await req.GetFromCloudAsync<DataCountry.Root>(code);

            var countryDomainCreator = new CountryFactory();
            return countryDomainCreator.Create(apiCountry);
        }
    }
}