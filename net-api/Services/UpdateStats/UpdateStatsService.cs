using IpInfoCore.Infraestructure.EventRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_api.Services.UpdateStats
{
    public class UpdateStatsService : IUpdateStatsService
    {
        private IEventRegister _eventRegister;

        public UpdateStatsService(IEventRegister eventRegister)
        {
            _eventRegister = eventRegister;
        }

        public void RegisterVisitor(string ip, string alpha2Code)
        {
            _eventRegister.RegisterEvent(alpha2Code, ip);
        }

    }
}
