using Autofac;
using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Portfolio.BusinessModel.Queries.Tests")]
[assembly: InternalsVisibleTo("Portfolio.Api.Tests")]

namespace Portfolio.BusinessModel.Queries.DependencyInjection
{
    public class AutofacDiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IQueryHandler>())
                .AsImplementedInterfaces();

            builder.RegisterType<QueryBus>().As<IQueryBus>();
            builder.Register(RegisterQueryHandlersFactoryDelegate());
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

    }
}
