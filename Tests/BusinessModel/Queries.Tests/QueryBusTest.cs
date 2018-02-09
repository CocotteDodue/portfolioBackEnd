using Moq;
using Portfolio.BusinessModel.Queries.Dummies;
using Portfolio.Tests.Dummies;
using System;
using Xunit;

namespace Portfolio.BusinessModel.Queries
{
    public class QueryBusTest
    {
        [Fact]
        public void Bus_ShouldHandleQueryRequest_WhenProcessIsCalled()
        {
            var fakeQueryHandler = new Mock<IQueryHandler<IQuery<BaseEntityDummy>, BaseEntityDummy>>();
            var fakeHandlesFactory = new Mock<Func<Type, IQueryHandler>>();
            fakeHandlesFactory
                .Setup(factory => factory(It.IsAny<Type>()))
                .Returns(fakeQueryHandler.Object);
            IQueryBus bus = new QueryBus(fakeHandlesFactory.Object);

            var result = bus.Process<IQuery<BaseEntityDummy>, BaseEntityDummy>(new EmptyQueryDummy());

            fakeQueryHandler.Verify(handler => handler.HandleQuery(It.IsAny<EmptyQueryDummy>()));
        }

        [Fact]
        public void Bus_ShouldHandleQueryRequestAsynchronously_WhenProcessAsyncIsCalled()
        {
            var fakeQueryHandler = new Mock<IQueryHandler<IQuery<BaseEntityDummy>, BaseEntityDummy>>();
            var fakeHandlesFactory = new Mock<Func<Type, IQueryHandler>>();
            fakeHandlesFactory
                .Setup(factory => factory(It.IsAny<Type>()))
                .Returns(fakeQueryHandler.Object);
            IQueryBus bus = new QueryBus(fakeHandlesFactory.Object);

            var result = bus.ProcessAsync<IQuery<BaseEntityDummy>, BaseEntityDummy>(new EmptyQueryDummy());

            fakeQueryHandler.Verify(handler => handler.HandleQueryAsync(It.IsAny<EmptyQueryDummy>()));
        }
    }
}
