using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Domain.Core.Entities
{
    public class Activity : Entity<Guid>
    {
        public Activity(string note, DateTime createdAt, Guid ticketId)
        {
            Note = note;
            CreatedAt = createdAt;
            TicketId = ticketId;
        }

        public Activity(string note)
        {
            Note = note;
            CreatedAt = DateTime.UtcNow;
        }

        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TicketId { get; set; }
    }
}
