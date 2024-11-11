using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleTicket.Application.Commands;
using SimpleTicket.Application.Core.Tickets.Common;
using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.Core.Repositories;
using SimpleTicket.Domain.SeedWork;

namespace SimpleTicket.Application.Core.Tickets.CreateTicket
{
    public class CreateTicketCommandHandler : ICommandHandler<CreateTicketCommand, TicketResponse>
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTicketCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, ILogger<CreateTicketCommandHandler> logger, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TicketResponse> ExecuteAsync(CreateTicketCommand command)
        {
            try
            {
                _logger.LogInformation("Start to create ticket with {@Request}", command);

                var ticket = new Ticket(command.Summary, command.Note, command.CreatorUser);

                await _unitOfWork.BeginTransaction();

                await _ticketRepository.AddAsync(ticket);

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("Sucess to create ticket");

                return _mapper.Map<TicketResponse>(ticket);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error when try to create ticket");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
