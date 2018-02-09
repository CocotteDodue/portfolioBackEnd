using System.Threading.Tasks;

namespace Portfolio.BusinessModel.Commands
{
    // Necessary for Reflexion to work and enable genericity (CommandBus processing method)
    public interface ICommandHandler
    { }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        void HandleCommand(TCommand command);
        Task HandleCommandAsync(TCommand command);
    }    
}
