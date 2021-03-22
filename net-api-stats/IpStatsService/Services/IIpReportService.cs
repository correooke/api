using IpStatsService.Domain;
using System.Threading.Tasks;

namespace IpStatsService.Services
{
    public interface IIpReportService
    {
        Task<IpCallsStats> GetIpCallsStats();
    }
}