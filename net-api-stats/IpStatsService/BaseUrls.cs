using System;
using System.Linq;

namespace net_api.Infraestructure.ExternalAPI
{

    public static class BaseUrls
    {
        static BaseUrls()
        {
            countryInfoUrlAll = Environment.GetEnvironmentVariable("URL_COUNTRY_INFO_ALL"); // "https://restcountries.eu/rest/v2/all";

            Validate(countryInfoUrlAll);
        }

        static void Validate(params string[] keys)
        {
            var emptyKeys = keys.Where(k => (k == null || k == string.Empty));

            if (emptyKeys.Any())
            {
                throw new Exception("Some environment keys are empty");
            }
        }

        public static readonly string countryInfoUrlAll;
    }
}
