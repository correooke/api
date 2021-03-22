namespace net_api.Services.UpdateStats
{
    public interface IUpdateStatsService
    {
        void RegisterVisitor(string ip, string alpha2Code);
    }
}