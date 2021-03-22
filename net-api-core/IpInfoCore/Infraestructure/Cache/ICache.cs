using System;
using System.Threading.Tasks;

namespace net_api.Infraestructure
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null);
        Task<string> GetAsyncFast<T>(string Key, Func<string, Task<T>> GetterAction, TimeSpan? expiresValue = null);
    }
}