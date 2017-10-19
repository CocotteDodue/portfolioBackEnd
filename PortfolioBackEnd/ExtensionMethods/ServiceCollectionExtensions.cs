using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace PortfolioBackEnd.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer AddIoC(this IServiceCollection serviceCollection)
        {
            //Create your Autofac Container
            ContainerBuilder containerBuilder = new ContainerBuilder();
            //Register your own services within Autofac


            //Put the framework services into Autofac
            containerBuilder.Populate(serviceCollection);
            return containerBuilder.Build();
        }

    }
}
