
/*IP: ${ip}
Fecha: ${formattedDate}
País:  ${name}
ISO Code: ${alpha2Code}
Idiomas: ${languageList}
Moneda: ${currenciesList}
Hora: ${timeZonesList}
Distancia estimada: ${distance} kms (-34, -64) a (${destination.latitude}, ${destination.longitude})`;
*/
using System;
using System.Collections.Generic;
using net_api.Domain;

namespace net_api.Controllers.Models
{
    public class IpInfo {
        public string IP { get; init; }
        public DateTime CurrentDate { get; init; }
        public String CountryName { get; init; }
        public String CountryCode { get; init; }
        public IEnumerable<String> Languages { get; init; }
        public IEnumerable<IpInfoCurrency> Currencies { get; init; }
        public IEnumerable<String> TimeZones { get; init; }

        public int Distance { get; init; }

        public double Latitude { get; init; }
        public double Longitude { get; init; }

    }
}