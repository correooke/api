using System.Threading.Tasks;

namespace net_api.Infraestructure.ExternalAPI
{
    public interface IAPIRequest
    {
        Task<T> GetFromCloudAsync<T>(params string[] urlParams);
    }
}