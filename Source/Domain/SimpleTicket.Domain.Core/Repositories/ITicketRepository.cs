using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Domain.Core.Repositories
{
    public interface ITicketRepository : IRepository<Ticket, Guid>
    {
        
    }
}
