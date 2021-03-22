using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService.Controllers.Models
{
    public class StatLine
    {
        public string Country { get; set; }
        public double Distance { get; set; }
        public int Calls { get; set; }
    }
}
