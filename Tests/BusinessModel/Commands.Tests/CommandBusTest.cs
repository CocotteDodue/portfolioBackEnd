using Moq;
using Portfolio.BusinessModel.Commands.Dummies;
using System;
using Xunit;

namespace Portfolio.BusinessModel.Commands
{
    public class CommandBusTest
    {
        [Fact]
        public void Bus_ShouldProcessCommand_WhenRunIsCalled()
        {
            var fakeCommandHandler = new Mock<ICommandHandler<ICommand>>();
            var fakeHandlesFactory = new Mock<Func<Type, ICommandHandler>>();
            fakeHandlesFactory
                .Setup(factory => factory(It.IsAny<Type>()))
                .Returns(fakeCommandHandler.Object);
            ICommandBus bus = new CommandBus(fakeHandlesFactory.Object);

            bus.Run<ICommand>(new EmptyCommandDummy());

            fakeCommandHandler.Verify(handler => handler.HandleCommand(It.IsAny<EmptyCommandDummy>()));
        }

        [Fact]
        public void Bus_ShouldHandleProcessCommandAsynchronously_WhenRunAsyncIsCalled()
        {
            var fakeCommandHandler = new Mock<ICommandHandler<ICommand>>();
            var fakeHandlesFactory = new Mock<Func<Type, ICommandHandler>>();
            fakeHandlesFactory
                .Setup(factory => factory(It.IsAny<Type>()))
                .Returns(fakeCommandHandler.Object);
            ICommandBus bus = new CommandBus(fakeHandlesFactory.Object);

            bus.RunAsync<ICommand>(new EmptyCommandDummy());

            fakeCommandHandler.Verify(handler => handler.HandleCommandAsync(It.IsAny<EmptyCommandDummy>()));
        }
    }
}
