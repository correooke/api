using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService.Domain
{
    public class IpCallsStats
    {
        public IEnumerable<CallsByCountry> CallsByCountryInfo { get; set; }
    }
}
