namespace SimpleTicket.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
        Task BeginTransaction();
    }
}
