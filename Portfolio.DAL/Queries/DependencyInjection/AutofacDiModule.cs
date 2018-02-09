using Autofac;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Portfolio.DAL.Queries.Tests")]
[assembly: InternalsVisibleTo("Portfolio.Api.Tests")]

namespace Portfolio.DAL.Queries.DependencyInjection
{
    public class AutofacDiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReadOnlyDatabase>().As<IReadOnlyDatabase>()
                .SingleInstance();
        }
    }
}
