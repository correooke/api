using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IpInfoCore.Infraestructure.EventRegister
{
    public class EventRegisterLight : IEventRegister
    {
        private readonly ConnectionMultiplexer _muxer;
        private IDatabase _conn;

        public EventRegisterLight(string connection)
        {
            _muxer = ConnectionMultiplexer.Connect(connection);
            _conn = _muxer.GetDatabase();
        }

        public void RegisterEvent(string eventType, object obj)
        {
            _conn.StringIncrement(getCounterKeyName(eventType));
        }

        public EventCounter EventsRegisteredByType(string eventType)
        {
            var counter = _conn.StringGet(getCounterKeyName(eventType));
            var intCounter = counter.HasValue ? int.Parse(counter) : 0;
            return new EventCounter { EventType = eventType, Quantity = intCounter };
        }
        
        private string getCounterKeyName(string eventType)
        {
            return $"counter{eventType}";
        }

    }
}
