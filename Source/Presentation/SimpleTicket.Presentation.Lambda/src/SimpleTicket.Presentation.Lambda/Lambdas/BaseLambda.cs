using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleTicket.Application.Commands;
using SimpleTicket.Infrastructure.Ioc.Container;
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
    
    public async Task Handle(SQSEvent @event, ILambdaContext context)
    {
        foreach (var message in @event.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSMessage message, ILambdaContext context)
    {
        try
        {
            using (var scope = _container.ServiceProvider.CreateAsyncScope())
            {
                var command = GetCommand(message);
                
                var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();
                var response = await commandHandler.ExecuteAsync(command);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    protected virtual TCommand GetCommand(SQSMessage message)
    {
        return JsonConvert.DeserializeObject<TCommand>(message.Body)!;
    }
}