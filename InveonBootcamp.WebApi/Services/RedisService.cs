using StackExchange.Redis;
using System.Text.Json;

namespace InveonBootcamp.WebApi.Services
{
    public class RedisService
    {
        private readonly IDatabase _database;

        public RedisService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonData, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _database.StringGetAsync(key);
            if (jsonData.IsNullOrEmpty) return default;

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
