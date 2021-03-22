using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService.Domain
{
    public class CallsByCountry
    {

        public string CountryCode { get; init; }

        public string CountryName { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public double GetDistance(GeoCoordinate From) {  
            var to = new GeoCoordinate(Latitude, Longitude);
            return Convert.ToInt32(From.GetDistanceTo(to) / 1000);

        }
        public int Calls { get; set; }
    }
}
