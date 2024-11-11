namespace SimpleTicket.Domain.SeedWork
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private readonly List<DomainEvent> _events = [];
        public IReadOnlyList<DomainEvent> Events { get => _events; }

        public void CleanEvents() => _events.Clear();
        public void AddEvent(DomainEvent e) => _events.Add(e);
    }
}
