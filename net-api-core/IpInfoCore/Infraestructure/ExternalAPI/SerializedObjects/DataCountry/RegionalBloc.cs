using System.Collections.Generic;

namespace net_api.Infraestructure.ExternalAPI.SerializedObjects.DataCountry
{
    public class RegionalBloc
    {
        public string acronym { get; set; }
        public string name { get; set; }
        public List<object> otherAcronyms { get; set; }
        public List<string> otherNames { get; set; }
    }
}
