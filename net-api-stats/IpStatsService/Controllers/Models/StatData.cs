﻿using System;
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
                return ByCountry.Where(c => c.Calls > 0).Max(c => c.Distance);
            }
        }

        public double MinDistance
        {
            get
            {
                return ByCountry.Where(c => c.Calls > 0).Min(c => c.Distance);
            }
        }

        public double AverageDistance
        {
            get
            {
                return (int) ByCountry.Where(c => c.Calls > 0).Sum(c => c.Distance * c.Calls) / TotalCalls;
            }
        }

        public int TotalCalls
        {
            get
            {
                return ByCountry.Sum(c => c.Calls);
            }
        }
    }
}
