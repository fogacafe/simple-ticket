﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace SimpleTicket.Infrastructure.Ioc.Configurations
{
    public static class LoggingConfiguration
    {
        public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration, string applicationName)
        {

            var options = new ConfigurationReaderOptions(typeof(ConsoleLoggerConfigurationExtensions).Assembly);
            var elasticAddress = configuration["Address:Elasticsearch"]!;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Information()
                .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticAddress))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{applicationName}",
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
