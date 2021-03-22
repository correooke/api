using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_api.Infraestructure.ExternalAPI.SerializedObjects.Ip2Country
{
    public class Root
    {
        public string countryCode { get; set; }
        public string countryCode3 { get; set; }
        public string countryName { get; set; }
        public string countryEmoji { get; set; }
    }

}
