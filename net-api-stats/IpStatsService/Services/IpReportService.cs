using IpInfoCore.Infraestructure.EventRegister;
using IpStatsService.Domain;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCountry = net_api.Infraestructure.ExternalAPI.SerializedObjects.DataCountry;

namespace IpStatsService.Services
{
    public class IpReportService : IIpReportService
    {
        private readonly IEventRegister _eventRegister;
        private readonly ICache _cache;
        private readonly IAPIRequestCreator _apiRequestCreator;

        public IpReportService(ICache cache, 
            IAPIRequestCreator apiRequestCreator, 
            IEventRegister eventRegister)
        {
            _eventRegister = eventRegister;
            _cache = cache;
            _apiRequestCreator = apiRequestCreator;
        }

        public async Task<IpCallsStats> GetIpCallsStats()
        {
            var stats = await _cache.GetAsync("IpCallsStats", (data) => GetterAction()); // Static information

            UpdateCallsCounter(stats);
            return stats;
        }

        private void UpdateCallsCounter(IpCallsStats stats)
        {
            if (stats == null || stats.CallsByCountryInfo == null)
                return;

            foreach (var country in stats.CallsByCountryInfo)
            {
                var events = _eventRegister.EventsRegisteredByType(country.CountryCode);
                country.Calls = events.Quantity;
            }
        }

        protected async Task<IpCallsStats> GetterAction()
        {
            var req = _apiRequestCreator.Create(BaseUrls.countryInfoUrlAll);

            var apiCountries = await req.GetFromCloudAsync<IEnumerable<DataCountry.Root>>();

            var ipCallsStatsFactory = new IpCallsStatsFactory();
            return ipCallsStatsFactory.Create(apiCountries);
        }

    }
}
