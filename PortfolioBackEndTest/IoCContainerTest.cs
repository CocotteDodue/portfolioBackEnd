using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using PortfolioBackEnd.ExtensionMethods;
using PortfolioBackEnd.Queries;
using Xunit;
using PortfolioBackEnd;
using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Commands;

namespace PortfolioBackEndTest
{
    public class IoCContainerTest
    {
        [Fact]
        public void IoCContainer_CanResolveReadOnlyDataBase_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var readonlyDatabase = appContainer.Resolve<IReadOnlyDatabase>();

            Assert.True(ObjectExistAndIsOfExpectedType(readonlyDatabase, typeof(ReadOnlyDatabase)));
        }

        [Fact]
        //TODO: Transform to check ALL query handler are resolved properly => 
        // 1 -> get all queryhandler via reflection
        // 2 -> foreach loop: iterate on all of them and combine result
        // 3 -> assert result
        public void IoCContainer_CanResolveQueryHandler_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var handler = appContainer.Resolve<IGetAllTechnologiesQueryHandler>();

            Assert.True(ObjectExistAndIsOfExpectedType(handler, typeof(GetAllTechnologiesQueryHandler)));
        }
        
        [Fact]
        public void IoCContainer_CanResolveQueryHandlerFactory_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);
            GetAllTechnologyiesQuery tq = new GetAllTechnologyiesQuery();

            var handlerFactory = appContainer.Resolve<Func<IGetAllTechnologyiesQuery, IGetAllTechnologiesQueryHandler>>();
            var handler = handlerFactory(tq);

            Assert.True(ObjectExistAndIsOfExpectedType(handler, typeof(GetAllTechnologiesQueryHandler)));
        }

        [Fact]
        public void IoCContainer_CanResolveQueryBus_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var queryBus = appContainer.Resolve<IQueryBus>();
            
            Assert.True(ObjectExistAndIsOfExpectedType(queryBus, typeof(QueryBus)));
        }

        [Fact]
        public void IoCContainer_CanResolveOperationDataBase_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var operationDatabase = appContainer.Resolve<IOperationsDatabase>();

            Assert.True(ObjectExistAndIsOfExpectedType(operationDatabase, typeof(OperationDatabase)));
        }

        [Fact]
        //TODO: Transform to check ALL query handler are resolved properly => 
        // 1 -> get all queryhandler via reflection
        // 2 -> foreach loop: iterate on all of them and combine result
        // 3 -> assert result
        public void IoCContainer_CanResolveCommandHandler_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var handler = appContainer.Resolve<IAddTechnologyVersionCommandHandler>();

            Assert.True(ObjectExistAndIsOfExpectedType(handler, typeof(AddTechnologyVersionCommandHandler)));
        }

        [Fact]
        public void IoCContainer_CanResolveCommandHandlerFactory_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);
            AddTechnologyVersionCommand addTechCommand = new AddTechnologyVersionCommand();

            var handlerFactory = appContainer.Resolve<Func<AddTechnologyVersionCommand, IAddTechnologyVersionCommand>>();
            var handler = handlerFactory(addTechCommand);

            Assert.True(ObjectExistAndIsOfExpectedType(handler, typeof(AddTechnologyVersionCommandHandler)));
        }

        //[Fact]
        //public void IoCContainer_CanResolveQueryBus_AfterInitialization()
        //{
        //    IServiceCollection services = InitializeServices();
        //    IContainer appContainer;
        //    services.AddIoC(out appContainer);

        //    var queryBus = appContainer.Resolve<IQueryBus>();

        //    Assert.True(ObjectExistAndIsOfExpectedType(queryBus, typeof(QueryBus)));
        //}

        private IServiceCollection InitializeServices()
        {
            IServiceCollection services = new ServiceCollection();
            services
                 .AddEntityFrameworkSqlServer()
                 .AddDbContext<PortfolioOperationsDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                 .AddDbContext<PortfolioReadOnlyDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            return services;
        }

        private bool ObjectExistAndIsOfExpectedType(object obj, Type expectedType)
        {
            return obj != null
                && obj.GetType().Equals(expectedType) ;
        }
    }
}
