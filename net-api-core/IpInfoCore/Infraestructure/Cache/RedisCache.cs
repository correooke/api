
using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace net_api.Infraestructure
{
    public class RedisCache : ICache
    {
        private readonly ConnectionMultiplexer _muxer;
        private IDatabase _conn;

        public RedisCache(string connection)
        {
            _muxer = ConnectionMultiplexer.Connect(connection);
            _conn = _muxer.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
        {
            var cachedValue = _conn.StringGet(Key);

            if (!cachedValue.IsNull)
                return JsonSerializer.Deserialize<T>(cachedValue);

            var value = await GetterAction(Key);

            _conn.StringSet(Key, JsonSerializer.Serialize<T>(value), expiresValue, flags: CommandFlags.FireAndForget);
           
            return value;
        }

        public async Task<string> GetAsyncFast<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null)
        {
            var cachedValue = _conn.StringGet(Key);

            if (!cachedValue.IsNull)
                return cachedValue;

            var value = await GetterAction(Key);

            _conn.StringSet(Key, JsonSerializer.Serialize<T>(value), expiresValue, flags: CommandFlags.FireAndForget);

            return JsonSerializer.Serialize<T>(value);
        }

    }
}