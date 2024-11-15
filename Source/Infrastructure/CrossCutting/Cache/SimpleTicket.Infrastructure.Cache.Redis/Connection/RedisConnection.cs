using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace SimpleTicket.Infrastructure.Cache.Redis.Connection
{
    public class RedisConnection : IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        private readonly ILogger<RedisConnection> _logger;

        public RedisConnection(string connectionString, ILogger<RedisConnection> logger)
        {
            _logger = logger;

            var options = ConfigurationOptions.Parse(connectionString);
            options.KeepAlive = 180;
            options.ReconnectRetryPolicy = new ExponentialRetry(5000);

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => CreateConnection(options));
        }

        private ConnectionMultiplexer CreateConnection(ConfigurationOptions options)
        {
            var connection = ConnectionMultiplexer.Connect(options);

            connection.ConnectionFailed += Connection_ConnectionFailed;
            connection.ConnectionRestored += Connection_ConnectionRestored;

            return connection;
        }

        private void Connection_ConnectionRestored(object? sender, ConnectionFailedEventArgs e)
        {
            _logger.LogInformation("Redis connection restored");
        }

        private void Connection_ConnectionFailed(object? sender, ConnectionFailedEventArgs e)
        {
            _logger.LogError(e.Exception, "Redis connection failed");
        }

        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        public IDatabase GetDatabase()
        {
            return Connection.GetDatabase();
        }

        public void Dispose()
        {
            if(Connection != null && Connection.IsConnected)
            {
                Connection.Close();
            }
        }
    }
}
