namespace SimpleTicket.Application.Commands
{
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand
    {
        Task<TResponse> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand {

        Task ExecuteAsync(TCommand command);

    }
}
