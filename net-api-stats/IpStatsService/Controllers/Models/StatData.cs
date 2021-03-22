using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService.Controllers.Models
{
    public class StatData
    {
        public DateTime LastUpdate { get; set; }

        public IEnumerable<StatLine> ByCountry { get; set; }

        public double MaxDistance
        {
            get
            {
                return ByCountry.Count() != 0 ? ByCountry.Where(c => c.Calls > 0).Max(c => c.Distance) : 0;
            }
        }

        public double MinDistance
        {
            get
            {
                return ByCountry.Count() != 0 ? ByCountry.Where(c => c.Calls > 0).Min(c => c.Distance) : 0;
            }
        }

        public double AverageDistance
        {
            get
            {
                return ByCountry.Count() != 0 ? (int) ByCountry.Where(c => c.Calls > 0).Sum(c => c.Distance * c.Calls) / TotalCalls : 0;
            }
        }

        public int TotalCalls
        {
            get
            {
                return ByCountry.Count() != 0 ? ByCountry.Sum(c => c.Calls) : 0;
            }
        }
    }
}
