using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;

namespace SimpleTicket.Infrastructure.Ioc.Container
{
    public static class LoggingConfiguration
    {
        public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration)
        {

            var options = new ConfigurationReaderOptions(typeof(ConsoleLoggerConfigurationExtensions).Assembly);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Information()
                .Filter.ByExcluding(log =>
                    log.Properties.ContainsKey("SourceContext") &&
                    (
                        log.Properties["SourceContext"].ToString().Contains("Microsoft")
                    )
                )
                .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"notification-{DateTime.UtcNow:yyyy-MM}",
                    NumberOfReplicas = 1,
                    NumberOfShards = 2
                })
                .CreateLogger();

            Log.Logger.Information("testando");

            services.AddLogging(x =>
            {
                x.AddSerilog();
            });

            return services;
        }
    }
}
