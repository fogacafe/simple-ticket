using Microsoft.EntityFrameworkCore;
using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Infrastructure.Data.EFCore.Configuration;

namespace SimpleTicket.Infrastructure.Data.EFCore.Contexts;

public class SimpleTicketContext(DbContextOptions<SimpleTicketContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TicketConfigurationModel());
        modelBuilder.ApplyConfiguration(new ActivityConfigurationModel());
        base.OnModelCreating(modelBuilder);
    }
}

    





