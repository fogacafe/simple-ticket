using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTicket.Application.Core.Tickets.Common;
using SimpleTicket.Application.Core.Tickets.CreateTicket;
using SimpleTicket.Infrastructure.Ioc.Configurations;

namespace SimpleTicket.Presentation.Lambda.Lambdas;

public class CreateTicketLambda : BaseLambda<CreateTicketCommand, TicketResponse>
{
    protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(configuration);
        services.AddCommandHandlers();
       

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddLogs(configuration, GetApplicationName());
    }

    protected override string GetApplicationName()
        => "spt-create-ticket-lambda";
}