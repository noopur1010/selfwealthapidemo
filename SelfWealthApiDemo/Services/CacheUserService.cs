using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthApiDemo
{
    public interface ICacheUserService
    {
        Task<User> GetCachedUser(string ke);
        Task SetCachedUser(string key, User user, DistributedCacheEntryOptions options);
        Task ClearCache(string key);
    }

    public class CacheUserService : ICacheUserService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheUserService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<User> GetCachedUser(string key)
        {
            return await _cacheProvider.GetFromCache<User>(key);
        }

        public async Task SetCachedUser(string key, User user, DistributedCacheEntryOptions options)
        {
            await _cacheProvider.SetCache(key, user, options);
        }

        public async Task ClearCache(string key)
        {
            await _cacheProvider.ClearCache(key);
        }
    }
}
