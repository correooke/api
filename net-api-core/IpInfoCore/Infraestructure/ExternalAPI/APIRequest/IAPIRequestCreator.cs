using Microsoft.Extensions.Logging;

namespace net_api.Infraestructure.ExternalAPI
{
    public interface IAPIRequestCreator
    {
        IAPIRequest Create(string baseUrl, ILogger logger = null);
    }
}
