using Microsoft.EntityFrameworkCore;
using SimpleTicket.Domain.Core.Repositories;
using SimpleTicket.Domain.SeedWork;
using SimpleTicket.Infrastructure.Data.EFCore;
using SimpleTicket.Infrastructure.Data.EFCore.Contexts;
using SimpleTicket.Infrastructure.Data.EFCore.Repositories;

namespace SimpleTicket.Presentation.Api.Configurations;

public static class RepositoriesConfiguration
{
    private const string SIMPLE_TICKET_DB_CONFIG_NAME = "SimpleTicketConnectionString"; 
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(SIMPLE_TICKET_DB_CONFIG_NAME);
        services.AddDbContext<SimpleTicketContext>(x => x.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        services.AddScoped<ITicketRepository, TicketRepository>();
               
        return services;
    }

    
}