namespace SimpleTicket.Domain.SeedWork
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        Task AddAsync(TEntity ticket);
        Task<TEntity?> FindAsync(TId id);
        Task UpdateAsync(TEntity ticket);
    }
}
