using System.Threading.Tasks;

namespace PortfolioBackEnd.Commands
{
    public interface ICommandBus
    {
        void Run<TCommand>(TCommand command) where TCommand : ICommand;
        Task RunAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
