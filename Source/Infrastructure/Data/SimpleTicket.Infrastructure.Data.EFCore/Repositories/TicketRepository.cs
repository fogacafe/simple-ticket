using Microsoft.EntityFrameworkCore;
using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.Core.Repositories;
using SimpleTicket.Infrastructure.Data.EFCore.Contexts;

namespace SimpleTicket.Infrastructure.Data.EFCore.Repositories;

public class TicketRepository(SimpleTicketContext context) : ITicketRepository
{
    public async Task AddAsync(Ticket ticket)
    {
        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();
    }

    public Task<Ticket?> FindAsync(Guid id)
    {
        return context
            .Tickets
            .Include(x => x.Activities)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        context.Entry(ticket).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}