
using net_api.Domain.Base;

namespace net_api.Domain {
    public class Language : IEntity
    {
        public string Name { get; init; }   
        public string IsoCode { get; init; } 
    }
}
