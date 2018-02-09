using Autofac;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Portfolio.DAL.Commands.Tests")]
[assembly: InternalsVisibleTo("Portfolio.Api.Tests")]

namespace Portfolio.DAL.Commands.DependencyInjection
{
    public class AutofacDiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // multiple instance makes sense? less heavy, more responsive, higher risk of inconsistency
            builder.RegisterType<OperationDatabase>().As<IOperationsDatabase>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            
        }
    }
}
