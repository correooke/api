using IpStatsService.Domain;
using System.Linq;
using System.Collections.Generic;
using DataCountry = net_api.Infraestructure.ExternalAPI.SerializedObjects.DataCountry;
using IpInfoCore.Infraestructure.EventRegister;

namespace IpStatsService.Services
{
    public class IpCallsStatsFactory
    {
        public IpCallsStatsFactory()
        {
           
        }

        public IpCallsStats Create(IEnumerable<DataCountry.Root> apiCountries)
        {
            var calls = new List<CallsByCountry>();
            
            foreach (var country in apiCountries)
            {
                var callByCountry = new CallsByCountry
                {
                    CountryCode = country.alpha2Code,
                    CountryName = country.name,
                    Latitude = country.latlng.Count > 0 ? country.latlng[0] : 0,
                    Longitude = country.latlng.Count > 0 ? country.latlng[1] : 0,
                    Calls = 0,
                };

                calls.Add(callByCountry);
            }

            var data = new IpCallsStats()
            {
                CallsByCountryInfo = calls
            };

            return data;
        }
    }
}