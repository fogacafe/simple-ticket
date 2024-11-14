namespace SimpleTicket.Infrastructure.Cache.Interfaces
{
    public interface ICacheService
    {

        Task SaveOrUpdateAsync<T>(T value, string key, TimeSpan? expire = null);
        Task<T?> TryGetValueAsync<T>(string key);
        Task DeleteAsync(string key);
    }
}
