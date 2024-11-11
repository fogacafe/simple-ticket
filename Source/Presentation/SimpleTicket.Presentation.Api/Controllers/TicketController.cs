using Microsoft.AspNetCore.Mvc;
using SimpleTicket.Application.Commands;
using SimpleTicket.Application.Core.Tickets.Common;
using SimpleTicket.Application.Core.Tickets.CreateTicket;

namespace SimpleTicket.Presentation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] ICommandHandler<CreateTicketCommand, TicketResponse> commandHandler,
        [FromBody] CreateTicketCommand command)
    {
        var response = await commandHandler.ExecuteAsync(command);
        return Ok(response);
    }
}