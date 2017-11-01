using System;
using System.Threading.Tasks;

namespace PortfolioBackEnd.Commands
{
    public class CommandBus : ICommandBus
    {
        private Func<Type, ICommandHandler> _handlersFactory;

        public CommandBus(Func<Type, ICommandHandler> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }

        public void Run<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = (ICommandHandler<TCommand>)_handlersFactory(typeof(TCommand));

            commandHandler.HandleCommand(command);
        }

        public async Task RunAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = (ICommandHandler<TCommand>)_handlersFactory(typeof(TCommand));

            await commandHandler.HandleCommandAsync(command);
        }
    }
}
