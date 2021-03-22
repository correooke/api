using System;
using System.Linq;

namespace net_api.Infraestructure.ExternalAPI
{

    public static class BaseUrls
    {
        static BaseUrls()
        {
            accessKey = Environment.GetEnvironmentVariable("ACCESS_KEY"); // "?access_key=a16086f9c469390399a857f9b76991f5&format=1";
            ip2CountryUrl = Environment.GetEnvironmentVariable("URL_IP2_COUNTRY"); // "https://api.ip2country.info/ip?{0}";
            countryInfoUrl = Environment.GetEnvironmentVariable("URL_COUNTRY_INFO"); // "https://restcountries.eu/rest/v2/alpha/{0}";
            countryInfoUrlAll = Environment.GetEnvironmentVariable("URL_COUNTRY_INFO_ALL"); // "https://restcountries.eu/rest/v2/all";
            currencyInfoUrl = Environment.GetEnvironmentVariable("URL_CURRENCY_INFO"); // "http://data.fixer.io/api/latest";

            Validate(accessKey, ip2CountryUrl, countryInfoUrl, countryInfoUrlAll, currencyInfoUrl);
        }

        static void Validate(params string[] keys)
        {
            var emptyKeys = keys.Where(k => (k == null || k == string.Empty));

            if (emptyKeys.Any())
            {
                throw new Exception("Some environment keys are empty");
            }
        }

        private static readonly string accessKey = "?access_key=a16086f9c469390399a857f9b76991f5&format=1";

        public static readonly string ip2CountryUrl = "https://api.ip2country.info/ip?{0}";
        public static readonly string countryInfoUrl = "https://restcountries.eu/rest/v2/alpha/{0}";
        public static readonly string countryInfoUrlAll = "https://restcountries.eu/rest/v2/all";
        public static readonly string currencyInfoUrl = "http://data.fixer.io/api/latest" + accessKey;
    }
}
