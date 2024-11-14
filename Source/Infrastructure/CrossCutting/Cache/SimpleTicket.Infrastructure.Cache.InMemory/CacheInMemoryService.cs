using SimpleTicket.Infrastructure.Cache.Interfaces;
using System.Runtime.Caching;

namespace SimpleTicket.Infrastructure.Cache.InMemory
{
    public class CacheInMemoryService : ICacheService
    {
        private readonly ObjectCache _cache;

        public CacheInMemoryService()
        {
            _cache = MemoryCache.Default;
        }

        public Task DeleteAsync(string key)
        {
            return Task.Run(() => _cache.Remove(key));
        }

        public Task SaveOrUpdateAsync<T>(T value, string key, TimeSpan? expiry = null)
        {
            var policy = new CacheItemPolicy();

            if (expiry.HasValue)
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(expiry.Value);
            }

            return Task.Run(() => _cache.Set(key, value, policy));
        }

        public async Task<T?> TryGetValueAsync<T>(string key)
        {
            var value = await Task.Run(() => _cache.Get(key));

            if (value != null && value is T)
                return (T)value;

            return default;
        }
    }
}
