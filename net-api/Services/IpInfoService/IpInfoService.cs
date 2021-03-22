using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using net_api.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using Ip2Country = net_api.Infraestructure.ExternalAPI.SerializedObjects.Ip2Country;

namespace net_api.Services
{
    public class IpInfoService : IIpInfoService
    {
        private readonly ICache _cache;
        private readonly ICountryService _countryService;
        private readonly ICurrencyRateService _currencyRateService;
        private readonly IAPIRequestCreator _apiRequestCreator;

        public IpInfoService(ICache cache, 
            IAPIRequestCreator apiRequestCreator,
            ICountryService countryService, 
            ICurrencyRateService currencyRateService)
        {
            _cache = cache;
            _apiRequestCreator = apiRequestCreator;
            _countryService = countryService;
            _currencyRateService = currencyRateService;
        }

        public async Task<Country> GetInfoByIp(string ip)
        {
            var countryCode = await GetCountryCodeByIp(ip);
            var countryInfo = await _countryService.GetByCode(countryCode);
            var currencies = await countryInfo.UpdateCurrencyRates(CurrencyRateUpdater);
            countryInfo.SetCurrencies(currencies);
            return countryInfo;
        }

        protected async Task<String> GetCountryCodeByIp(string ip)
        {
            var basicInfo = await _cache.GetAsync(ip, ip => GetterAction(ip), TimeSpan.FromHours(3));
            return basicInfo.countryCode;
        }

        private async Task<Ip2Country.Root> GetterAction(string ip)
        {
            var req = _apiRequestCreator.Create(BaseUrls.ip2CountryUrl);

            var apiCountryBasicInfo = await req.GetFromCloudAsync<Ip2Country.Root>(ip);
            return apiCountryBasicInfo;
        }

        protected async Task<Double> CurrencyRateUpdater(string currencyCode)
        {
            var rate = await _currencyRateService.GetRateByCurrencyCode(currencyCode);
            return rate;
        }

    }
}