using net_api.Domain.Base;

namespace net_api.Domain
{
    public class Currency : IEntity
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string Symbol { get; init; }
        public double Rate { get; internal set; }

        public Currency Copy()
        {
            return (Currency)this.MemberwiseClone();
        }
    }
}
