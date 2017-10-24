using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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

            //register bus
            containerBuilder.RegisterType<ReadOnlyDatabase>().As<IReadOnlyDatabase>();

            //Queryhandler
            containerBuilder.RegisterType<GetAllTechnologiesQueryHandler>().As<IGetAllTechnologiesQueryHandler>();


            containerBuilder.RegisterType<QueryBus>().As<IQueryBus>();


            //Put the framework services into Autofac
            containerBuilder.Populate(serviceCollection);
            appContainer = containerBuilder.Build();

            return new AutofacServiceProvider(appContainer);
        }

    }
}
