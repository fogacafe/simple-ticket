using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Domain.Core.Entities
{
    public class Activity : Entity<Guid>
    {
        public Activity(string note, DateTime createdAt, Guid ticketId)
        {
            Id = Guid.NewGuid();
            Note = note;
            CreatedAt = createdAt;
            TicketId = ticketId;
        }

        public Activity(string note)
        {
            Id = Guid.NewGuid();
            Note = note;
            CreatedAt = DateTime.UtcNow;
        }

        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TicketId { get; set; }
    }
}
