using SimpleTicket.Domain.Core.Enums;
using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Domain.Core.Entities
{
    public class Ticket : AggregateRoot<Guid>
    {
        public Ticket(string summary, string note, string creatorUsername)
        {
            Id = Guid.NewGuid();
            Summary = summary;
            Note = note;
            Deadline = null;
            CreatedAt = DateTime.UtcNow;
            FinishedAt = null;
            CategoryId = null;
            Category = null;
            Priority = null;
            Status = TicketStatus.Open;
            ResponsibleUsername = null;
            CreatorUsername = creatorUsername;

            Activities = [];
        }

        public Ticket()
        {
            Id = Guid.NewGuid();
            Summary = string.Empty;
            Note = string.Empty;
            Deadline = null;
            CreatedAt = DateTime.UtcNow;
            FinishedAt = null;
            CategoryId = null;
            Category = null;
            Priority = null;
            Status = TicketStatus.Open;
            ResponsibleUsername = null;
            CreatorUsername = string.Empty;
            Activities = [];
        }

        public string Summary { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Priority? Priority { get; set; }
        public TicketStatus Status { get; set; }
        public List<Activity> Activities { get; set; }
        public string? ResponsibleUsername { get; set; }
        public string CreatorUsername { get; set; }

        public void SetNote(string note)
        {
            Note = note;
        }

        public void SetPriority(Priority priority)
        {
            Priority = priority;

            AddActivity(new Activity($"Priority set to {priority}"));
        }

        public void SetCategory(Category category)
        {
            CategoryId = category.Id;
            Category = category;
            Deadline = CreatedAt.AddDays(category.DeadlineDays);

            AddActivity(new Activity($"Set category to {category.Name}"));
        }

        public void AssignTo(string username)
        {
            ResponsibleUsername = username;
            AddActivity(new Activity($"Ticket was assigned to {username}"));
        }

        public void Finish()
        {
            if (Status == TicketStatus.Closed)
                throw new Exception("Ticket is already closed");

            FinishedAt = DateTime.UtcNow;
            Status = TicketStatus.Closed;

            AddActivity(new Activity("Ticket was finished"));
        }

        public void ReOpen()
        {
            FinishedAt = null;
            Status = TicketStatus.Open;

            AddActivity(new Activity("Ticket was reopened"));
        }

        public void AddActivity(Activity activity)
        {
            activity.TicketId = Id;
            Activities.Add(activity);
        }
    }
}
