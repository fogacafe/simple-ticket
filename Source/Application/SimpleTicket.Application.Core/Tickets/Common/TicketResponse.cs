using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.Core.Enums;

namespace SimpleTicket.Application.Core.Tickets.Common
{
    public record TicketResponse
    {
        public Guid Id { get; init; }
        public string? Summary { get; init; }
        public string? Note { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? Deadline { get; init; }
        public DateTime? FinishedAt { get; init; }
        public Guid? CategoryId { get; init; }
        public Category? Category { get; init; }
        public Priority? Priority { get; init; }
        public TicketStatus Status { get; init; }
        public List<ActivityResponse>? Activities { get; init; }
        public string? ResponsibleUsername { get; init; }
        public string? CreatorUsername { get; init; }

        
    }
}
