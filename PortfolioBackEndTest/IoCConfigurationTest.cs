using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using PortfolioBackEnd.ExtensionMethods;
using PortfolioBackEnd.Queries;
using Xunit;
using PortfolioBackEnd;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBackEndTest
{
    public class IoCConfigurationTest
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

        // TODO: attempt to test handlerFactory resolution

        [Fact]
        public void IoCContainer_CanResolveQueryBus_AfterInitialization()
        {
            IServiceCollection services = InitializeServices();
            IContainer appContainer;
            services.AddIoC(out appContainer);

            var queryBus = appContainer.Resolve<IQueryBus>();
            
            Assert.True(ObjectExistAndIsOfExpectedType(queryBus, typeof(QueryBus)));
        }

        //[Fact]
        //public void IoC_ContainsQueryBus_AfterInitialization ()
        //{
        //    IServiceCollection sc = new ServiceCollection();
        //    IContainer appContainer;
        //    sc.AddIoC(out appContainer);

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
