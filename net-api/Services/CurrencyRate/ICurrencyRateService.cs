using System.Threading.Tasks;

namespace net_api.Services
{
    public interface ICurrencyRateService
    {
        Task<double> GetRateByCurrencyCode(string code);
    }
}