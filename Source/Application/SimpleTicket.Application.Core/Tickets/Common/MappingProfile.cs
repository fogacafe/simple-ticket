using AutoMapper;
using SimpleTicket.Domain.Core.Entities;

namespace SimpleTicket.Application.Core.Tickets.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Activity, ActivityResponse>();
            CreateMap<Ticket, TicketResponse>();
        }
    }
}
