using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpInfoCore.Infraestructure
{
    public static class Env
    {
        private static string RedisInfoServer;
        private static string RedisStatsServer;

        static Env()
        {
            RedisInfoServer = Environment.GetEnvironmentVariable("REDIS_INFO_SERVER");
            RedisStatsServer = Environment.GetEnvironmentVariable("REDIS_STATS_SERVER");
        }

        public static string RedisInfo
        {
            get
            {
                return RedisInfoServer;
            }
        }

        public static string RedisStats
        {
            get
            {
                return RedisStatsServer;
            }
        }
    }
}
