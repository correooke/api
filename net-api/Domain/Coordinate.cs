using net_api.Domain.Base;

namespace net_api.Domain
{
    public class Coordinate : IEntity
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}