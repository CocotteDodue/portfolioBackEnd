using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PortfolioBackEnd.Commands;
using PortfolioBackEnd.Queries;
using System;

namespace PortfolioBackEnd.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddIoC(this IServiceCollection serviceCollection, out IContainer appContainer)
        {
            //Create your Autofac Container
            ContainerBuilder containerBuilder = new ContainerBuilder();
            //Register your own services within Autofac

            //register db abstraction
            containerBuilder.RegisterType<ReadOnlyDatabase>().As<IReadOnlyDatabase>();
            containerBuilder.RegisterType<OperationDatabase>().As<IOperationsDatabase>();

            //Queryhandler
            containerBuilder.RegisterType<GetAllTechnologiesQueryHandler>().As<IGetAllTechnologiesQueryHandler>();


            //Queryhandler
            containerBuilder.RegisterType<AddTechnologyVersionCommandHandler>().As<IAddTechnologyVersionCommandHandler>();

            // register bus
            containerBuilder.RegisterType<QueryBus>().As<IQueryBus>();

            // register delegate for bus
            containerBuilder.Register(RegisterHandlersFactoryDelegate());


            //Put the framework services into Autofac
            containerBuilder.Populate(serviceCollection);
            appContainer = containerBuilder.Build();

            return new AutofacServiceProvider(appContainer);
        }

        /// <summary>
        /// access scoped container at runtime via resolution of IComponentContext, resolving queryFactoryDelegate
        /// </summary>
        /// <returns>delegate resolving QueryHandler</returns>
        private static Func<IComponentContext, Func<Type, IQueryHandler>> RegisterHandlersFactoryDelegate()
        {
            return ctx =>
            {
                var container = ctx.Resolve<IComponentContext>();

                return ResolveQueryHandlerAtRunTime(container);
            };
        }

        /// <summary>
        /// provides a delegate resolving QueryHandler at runtime based on the QueryType and Result type expected
        /// </summary>
        /// <param name="container">IoCContainer as IComponentContext used to resolve the query handler at runtime</param>
        /// <returns>delegate resolving QueryHandler</returns>
        private static Func<Type, IQueryHandler> ResolveQueryHandlerAtRunTime(IComponentContext container)
        {
            return queryType =>
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType);

                return (IQueryHandler)container.Resolve(handlerType);
            };
        }

    }
}
