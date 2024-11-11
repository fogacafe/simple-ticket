namespace SimpleTicket.Application.Core.Tickets.Common
{
    public record ActivityResponse
    {
        public Guid Id { get; init; }
        public string? Note { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
