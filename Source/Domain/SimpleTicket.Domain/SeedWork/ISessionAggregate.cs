namespace SimpleTicket.Domain.SeedWork
{
    public interface ISessionAggregate
    {
        void Add<T>(AggregateRoot<T> aggregateRoot);
        Task CommitAsync();
        void ClearEvents();
    }
}
