using net_api.Domain;
using System.Threading.Tasks;

namespace net_api.Services
{
    public interface IIpInfoService
    {
        Task<Country> GetInfoByIp(string ip);
    }
}