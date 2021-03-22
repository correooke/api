using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_api.Infraestructure.ExternalAPI
{
    public interface IAPIRequestCreator
    {
        IAPIRequest Create(string baseUrl);
    }
}
