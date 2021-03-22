using net_api.Domain;
using System.Collections.Generic;
using DataCountry = net_api.Infraestructure.ExternalAPI.SerializedObjects.DataCountry;
using System.Linq;
using System;

namespace net_api.Services
{
    /// <summary>
    /// Desacople clase de mapeo de API y clase Domain
    /// </summary>
    public class CountryFactory
    {
        public Country Create(DataCountry.Root apiCountry)
        {
            validate(apiCountry);

            var country = new Country()
            {
                Name = apiCountry.name,
                Alpha2Code = apiCountry.alpha2Code,
                Coordinate = new Coordinate()
                {
                    Latitude =  apiCountry.latlng[0],
                    Longitude = apiCountry.latlng[1]
                },
                TimeZones = apiCountry.timezones.Select(t => t),
                Languages = CreateLanguages(apiCountry.languages),
                Currencies = CreateCurrencies(apiCountry.currencies),
            };

            return country;
        }

        private void validate(DataCountry.Root apiCountry)
        {
            var complete = apiCountry != null &&
                            apiCountry.latlng?.Count > 1 &&
                            apiCountry.timezones?.Count > 0 &&
                            apiCountry.languages?.Count > 0 &&
                            apiCountry.currencies?.Count > 0;

            if (!complete)
                throw new Exception("Incomplete base country data");
        }

        protected IEnumerable<Language> CreateLanguages(List<DataCountry.Language> languages)
        {
            var domainLanguages = languages.Select<DataCountry.Language, Language>(
                lang => (
                new Language()
                {
                    Name = lang.name,
                    IsoCode = lang.iso639_1
                }
            ));
            return domainLanguages;
        }

        protected IEnumerable<Currency> CreateCurrencies(List<DataCountry.Currency> currencies)
        {
            var domainCurrencies = currencies.Select<DataCountry.Currency, Currency>(
                currency => (
                new Currency()
                {
                    Code = currency.code,
                    Name = currency.name,
                    Symbol = currency.symbol,
                }
            ));
            return domainCurrencies;
        }
    }
}
