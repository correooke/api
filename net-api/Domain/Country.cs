using net_api.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_api.Domain
{
    public class Country : IAggregateRoot
    {
        public string Name { get; init; } 
        public string Alpha2Code { get; init; }
        public Coordinate Coordinate { get; init; }
        public IEnumerable<Language> Languages { get; init; } 
        public IEnumerable<Currency> Currencies { get; set; }
        public IEnumerable<string> TimeZones { get; init; }

        public async Task<IEnumerable<Currency>> UpdateCurrencyRates(Func<string, Task<double>> currencyRateUpdater)
        {
            var updatedCurrencies = new List<Currency>();
            if (Currencies != null)
            {
                foreach (var currency in Currencies)
                {
                    var newCurrency = currency.Copy();
                    newCurrency.Rate = await currencyRateUpdater(currency.Code);
                    updatedCurrencies.Add(newCurrency);
                }
            }
            return updatedCurrencies;
        }

        public void SetCurrencies(IEnumerable<Currency> currencies)
        {
            this.Currencies = currencies;
        }
    }
}
