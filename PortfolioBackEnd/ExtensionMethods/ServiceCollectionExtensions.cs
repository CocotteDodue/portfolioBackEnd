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


            //CommandHandlerhandler
            containerBuilder.RegisterType<AddTechnologyVersionCommandHandler>().As<IAddTechnologyVersionCommandHandler>();

            // register bus
            containerBuilder.RegisterType<QueryBus>().As<IQueryBus>();
            containerBuilder.RegisterType<CommandBus>().As<ICommandBus>();

            // register delegate for bus
            containerBuilder.Register(RegisterQueryHandlersFactoryDelegate());
            containerBuilder.Register(RegisterCommandHandlersFactoryDelegate());


            //Put the framework services into Autofac
            containerBuilder.Populate(serviceCollection);
            appContainer = containerBuilder.Build();

            return new AutofacServiceProvider(appContainer);
        }

        /// <summary>
        /// access scoped container at runtime via resolution of IComponentContext, resolving queryFactoryDelegate
        /// </summary>
        /// <returns>delegate resolving QueryHandler</returns>
        private static Func<IComponentContext, Func<Type, IQueryHandler>> RegisterQueryHandlersFactoryDelegate()
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

        /// <summary>
        /// access scoped container at runtime via resolution of IComponentContext, resolving commandFactoryDelegate
        /// </summary>
        /// <returns>delegate resolving CommandHandler</returns>
        private static Func<IComponentContext, Func<Type, ICommandHandler>> RegisterCommandHandlersFactoryDelegate()
        {
            return ctx =>
            {
                var container = ctx.Resolve<IComponentContext>();
                return ResolveCommandHandlerAtRunTime(container);
            };
        }

        /// <summary>
        /// provides a delegate resolving CommandHandler at runtime based on the CommandType expected
        /// </summary>
        /// <param name="container">IoCContainer as IComponentContext used to resolve the query handler at runtime</param>
        /// <returns>delegate resolving CommandHandler</returns>
        private static Func<Type, ICommandHandler> ResolveCommandHandlerAtRunTime(IComponentContext container)
        {
            return commandType =>
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
                return (ICommandHandler)container.Resolve(handlerType);
            };
        }
    }
}
