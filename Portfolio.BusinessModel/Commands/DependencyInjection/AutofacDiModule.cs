using Autofac;
using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Portfolio.BusinessModel.Commands.Tests")]
[assembly: InternalsVisibleTo("Portfolio.Api.Tests")]


namespace Portfolio.BusinessModel.Commands.DependencyInjection
{
    public class AutofacDiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<ICommandHandler>())
                .AsImplementedInterfaces();

            builder.RegisterType<CommandBus>().As<ICommandBus>();
            builder.Register(RegisterCommandHandlersFactoryDelegate());
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
