using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_api.Infraestructure.ExternalAPI
{
    public class APIRequestCreator : IAPIRequestCreator
    {
        public IAPIRequest Create(string baseUrl, ILogger logger)
        {
            return new APIRequest() { 
                BaseUrl = baseUrl,
                Logger = logger,
            };
        }
    }
}
