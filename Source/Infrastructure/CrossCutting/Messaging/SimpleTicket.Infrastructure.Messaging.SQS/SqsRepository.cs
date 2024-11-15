using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SimpleTicket.Infrastructure.Messaging.SQS
{
    public class SqsRepository : ISqsRepository
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly ILogger<SqsRepository> _logger;

        public SqsRepository(IAmazonSQS amazonSQS, ILogger<SqsRepository> logger)
        {
            _amazonSQS = amazonSQS;
            _logger = logger;
        }

        public async Task PublishAsJsonAsync<T>(T value, string queue, int? delay = null, string? messageGroup = null, string? theduplication = null)
        {
            var request = new SendMessageRequest
            {
                QueueUrl = queue,
                MessageBody = JsonConvert.SerializeObject(value),
                DelaySeconds = delay ?? 0,
                MessageDeduplicationId = theduplication,
                MessageGroupId = messageGroup,
            };

            var response = await _amazonSQS.SendMessageAsync(request);
            _logger.LogInformation("Sqs message publish {@Response}", response);
        }

        public async Task PublishAsync(string value, string queue, int? delay = null, string? messageGroup = null, string? theduplication = null)
        {
            var request = new SendMessageRequest
            {
                QueueUrl = queue,
                MessageBody = value,
                DelaySeconds = delay ?? 0,
                MessageDeduplicationId = theduplication,
                MessageGroupId = messageGroup,
            };

            var response = await _amazonSQS.SendMessageAsync(request);
            _logger.LogInformation("Sqs message publish {@Response}", response);
        }
    }
}
