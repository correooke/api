using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using net_api.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using DataRates = net_api.Infraestructure.ExternalAPI.SerializedObjects.DataRates;

namespace net_api.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ICache _cache;
        private readonly IAPIRequestCreator _apiRequestCreator;
        private Dictionary<string, double> _rates = null;

        public CurrencyRateService(ICache cache, IAPIRequestCreator apiRequestCreator)
        {
            _cache = cache;
            _apiRequestCreator = apiRequestCreator;
        }

        public async Task<double> GetRateByCurrencyCode(string code)
        {
            _rates ??= await _cache.GetAsync(code, (key) => GetterAction(), TimeSpan.FromHours(1)); // Refresh on hourly bases
            return _rates[code];
        }

        private async Task<Dictionary<string, double>> GetterAction()
        {
            var req = _apiRequestCreator.Create(BaseUrls.currencyInfoUrl);
            var apiRates = await req.GetFromCloudAsync<DataRates.Root>();
            return CreateRatesDictionary(apiRates);
        }

        private Dictionary<string, double> CreateRatesDictionary(DataRates.Root apiRates)
        {
            Validate(apiRates);

            var props = apiRates.rates.GetType().GetProperties();
            var rates = new Dictionary<string, double>();

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var rate = (double)prop.GetValue(apiRates.rates);
                rates.Add(prop.Name, rate);
            }

            return rates;
        }

        private void Validate(DataRates.Root apiRates)
        {
            throw new Exception(@"Error getting currency rates: " + apiRates.ToString());
        }
    }
}