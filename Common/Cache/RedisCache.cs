using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Common.Cache
{
   public class RedisCache : IRedisCache
   {
      private IDistributedCache _distributedCache;
      public RedisCache(IDistributedCache distributedCache)
      {
         _distributedCache = distributedCache;
      }

      public async Task<T> GetCacheAsync<T>(string key)
      {
         T result = default(T);
         var jsonCache = await _distributedCache.GetStringAsync(key);
         if (jsonCache != null)
            result = JsonConvert.DeserializeObject<T>(jsonCache);

         return result;
      }

      public async Task SetCacheAsync<T>(string key, T value)
      {
         var jsonCache = await _distributedCache.GetStringAsync(key);
         if (string.IsNullOrEmpty(jsonCache))
         {
            var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            var objectJson = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, objectJson, option);
         }
      }
   }
}