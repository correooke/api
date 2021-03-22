using IpInfoCore.Infraestructure.EventRegister;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using net_api.Services.UpdateStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_api.Services.UpdateStats.Tests
{
    [TestClass()]
    public class UpdateStatsServiceTests
    {
        [TestMethod()]
        public void RegisterVisitorTest()
        {
            var register = new Mock<IEventRegister>();

            var service = new UpdateStatsService(register.Object);

            register.Setup((r) => r.RegisterEvent("US", "1.2.3.4"));


            service.RegisterVisitor("1.2.3.4", "US");

            register.Verify((r) => r.RegisterEvent("US", "1.2.3.4"), Times.Once);
            
        }
    }
}