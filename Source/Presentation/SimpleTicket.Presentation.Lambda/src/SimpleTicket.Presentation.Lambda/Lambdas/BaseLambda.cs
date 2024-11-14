using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.Lambda.SQSEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using SimpleTicket.Application.Commands;
using SimpleTicket.Infrastructure.Ioc.Container;
using System.Diagnostics;
using SQSMessage = Amazon.Lambda.SQSEvents.SQSEvent.SQSMessage;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace SimpleTicket.Presentation.Lambda.Lambdas;

public abstract class BaseLambda<TCommand, TResponse>
    where TCommand : ICommand
{
    private readonly Container _container;
    protected BaseLambda()
    {
        _container = new Container();
        _container.Build(ConfigureServices);
    }

    protected abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    protected abstract string GetApplicationName();

    public async Task Handle(SQSEvent @event, ILambdaContext context)
    {
        using(LogContext.PushProperty("RequestId", context.AwsRequestId))
        {
            var watch = new Stopwatch();
            watch.Start();

            Log.Logger.Information("Start process Request with {@Records}", @event.Records);

            foreach (var message in @event.Records)
            {
                await ProcessMessageAsync(message, context);
            }

            watch.Stop();
            Log.Logger.Information("End process execution {TotalTime} and {TimeAverage} by request", watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / @event.Records.Count);
            
        }
    }

    private async Task ProcessMessageAsync(SQSMessage message, ILambdaContext context)
    {
        try
        {
            var command = GetCommand(message);

            using (var scope = _container.ServiceProvider.CreateAsyncScope())
            using (LogContext.PushProperty("EventId", GetEventId(command)))
            {
                var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();
                var response = await commandHandler.ExecuteAsync(command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected virtual TCommand GetCommand(SQSMessage message)
    {
        return JsonConvert.DeserializeObject<TCommand>(message.Body)!;
    }

    protected virtual string GetEventId(TCommand command)
    {
        return Guid.NewGuid().ToString();
    }
}