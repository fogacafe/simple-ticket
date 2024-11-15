namespace SimpleTicket.Infrastructure.Cache.Interfaces
{
    public interface ICacheService
    {

        Task<bool> SaveOrUpdateAsync<T>(T value, string key, TimeSpan? expiry = null);
        Task<T?> TryGetValueAsync<T>(string key);
        Task<bool> DeleteAsync(string key);
    }
}
