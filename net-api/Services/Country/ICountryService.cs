using net_api.Domain;
using System.Threading.Tasks;

namespace net_api.Services
{
    public interface ICountryService
    {
        Task<Country> GetByCode(string code);
    }
}