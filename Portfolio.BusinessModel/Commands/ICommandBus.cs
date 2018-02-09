using System.Threading.Tasks;

namespace Portfolio.BusinessModel.Commands
{
    public interface ICommandBus
    {
        void Run<TCommand>(TCommand command) where TCommand : ICommand;
        Task RunAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
