namespace SimpleTicket.Infrastructure.Messaging.SQS
{
    public interface ISqsRepository
    {
        Task PublishAsJsonAsync<T>(T value, string queue, int? delay = null, string? messageGroup = null, string? theduplication = null);
        Task PublishAsync(string value, string queue, int? delay = null, string? messageGroup = null, string? theduplication = null);
    }
}
