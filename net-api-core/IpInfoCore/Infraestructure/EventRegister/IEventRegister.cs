using System.Collections.Generic;

namespace IpInfoCore.Infraestructure.EventRegister
{
    public interface IEventRegister
    {
        void RegisterEvent(string eventType, object obj);

        EventCounter EventsRegisteredByType(string eventType);
    }
}