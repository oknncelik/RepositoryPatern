using System.Threading.Tasks;

namespace Common.Cache
{
    public interface IRedisCache
    {
         Task<T> GetCacheAsync<T>(string key);
         Task SetCacheAsync<T>(string key, T value);
    }
}