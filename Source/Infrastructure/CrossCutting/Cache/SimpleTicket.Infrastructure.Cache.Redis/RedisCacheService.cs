using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleTicket.Infrastructure.Cache.Interfaces;
using SimpleTicket.Infrastructure.Cache.Redis.Connection;
using StackExchange.Redis;

namespace SimpleTicket.Infrastructure.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly TimeSpan _defaultExpiry;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(RedisConnection connection, ILogger<RedisCacheService> logger)
        {
            _logger = logger;
            _database = connection.GetDatabase();
            _defaultExpiry = TimeSpan.FromMinutes(5);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            try
            {
                await _database.StringGetDeleteAsync(key);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error when try to delete redis cache with {key}", key);
                return false;
            }
        }

        public async Task<bool> SaveOrUpdateAsync<T>(T value, string key, TimeSpan? expiry = null)
        {
            try
            {
                var jsonValue = JsonConvert.SerializeObject(value);
                var ttl = expiry ?? _defaultExpiry;

                await _database.StringSetAsync(key, jsonValue, ttl);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error when try to save or update redis cache with {key}", key);
                return false;
            }
        }

        public async Task<T?> TryGetValueAsync<T>(string key)
        {
            try
            {
                var taskDelay = Task.Delay(TimeSpan.FromSeconds(1));
                var taskRedis = _database.StringGetAsync(key);

                var completedTask = await Task.WhenAny(taskDelay, taskRedis);

                if (completedTask == taskDelay)
                    throw new TimeoutException();

                var redisValue = taskRedis.Result;

                if(redisValue.IsNullOrEmpty)
                    return default;

                return JsonConvert.DeserializeObject<T>(redisValue!);

            }
            catch(TimeoutException ex)
            {
                _logger.LogError(ex, "Redis maximum consultation time expired for {key}", key);
                return default;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error when try to read {key}", key);
                return default;
            }
        }
    }
}
