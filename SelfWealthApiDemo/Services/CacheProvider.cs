using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace SelfWealthApiDemo
{
    public interface ICacheProvider
    {
        Task<T> GetFromCache<T>(string key) where T : class;
        Task SetCache<T>(string key, T value, DistributedCacheEntryOptions options) where T : class;
        Task ClearCache(string key);
    }

    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheProvider> _logger;

        public CacheProvider(ILogger<CacheProvider> logger, IDistributedCache cache)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<T> GetFromCache<T>(string key) where T : class
        {
            try
            {
                var cachedUsers = await _cache.GetStringAsync(key);
                return cachedUsers == null ? null : JsonSerializer.Deserialize<T>(cachedUsers);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return await Task.FromResult<T>(null);
            }
        }

        public async Task SetCache<T>(string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            try
            {
                var users = JsonSerializer.Serialize(value);
                if (options == null)
                {
                    await _cache.SetStringAsync(key, users);
                }
                else
                {
                    await _cache.SetStringAsync(key, users, options);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
            }
        }

        public async Task ClearCache(string key)
        {
            try
            {
                await _cache.RemoveAsync(key);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
            }
        }
    }
}
