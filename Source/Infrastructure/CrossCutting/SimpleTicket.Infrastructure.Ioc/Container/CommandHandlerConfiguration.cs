using Microsoft.Extensions.DependencyInjection;
using SimpleTicket.Application.Commands;
using SimpleTicket.Application.Core.Tickets.Common;
using SimpleTicket.Application.Core.Tickets.CreateTicket;

namespace SimpleTicket.Infrastructure.Ioc.Container;

public static class CommandHandlerConfiguration
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        AddTicketCommandHandlers(services);
        return services;
    }

    private static void AddTicketCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateTicketCommand, TicketResponse>, CreateTicketCommandHandler>();
    }
}