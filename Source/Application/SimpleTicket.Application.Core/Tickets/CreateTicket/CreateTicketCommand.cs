using SimpleTicket.Application.Commands;

namespace SimpleTicket.Application.Core.Tickets.CreateTicket
{
    public class CreateTicketCommand : ICommand
    {
        public CreateTicketCommand(string summary, string note, string creatorUser)
        {
            Summary = summary;
            Note = note;
            CreatorUser = creatorUser;
        }

        public string Summary { get; set; }
        public string Note { get; set; }
        public string CreatorUser { get; set; }
    }
}
