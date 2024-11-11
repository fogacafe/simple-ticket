using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Domain.Core.Entities
{
    public class Category : Entity<Guid>
    {
        public Category(string name, int deadlineDays)
        {
            Name = name;
            DeadlineDays = deadlineDays;
        }

        public string Name { get; private set; }
        public int DeadlineDays { get; private set; }
    }
}
