using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.DAL.Commands;
using Portfolio.DAL.Queries;
using System;
using System.Collections.Generic;
using BusinessModelCommands = Portfolio.BusinessModel.Commands.DependencyInjection;
using BusinessModelQueries = Portfolio.BusinessModel.Queries.DependencyInjection;
using DALCommands = Portfolio.DAL.Commands.DependencyInjection;
using DALQueries = Portfolio.DAL.Queries.DependencyInjection;

namespace Portfolio.Api.DiRegistration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceProvider RegisterAutofacIoCContainer(this IServiceCollection servicesCollection, out IContainer appContainer)
        {
            //Create your Autofac Container
            ContainerBuilder containerBuilder = new ContainerBuilder();

            //Put the framework services into Autofac
            containerBuilder.Populate(servicesCollection);

            // load DI modules
            IList<IModule> modulesToLoad = new List<IModule>();

            modulesToLoad.Add(new DALQueries.AutofacDiModule());
            modulesToLoad.Add(new DALCommands.AutofacDiModule());
            modulesToLoad.Add(new BusinessModelQueries.AutofacDiModule());
            modulesToLoad.Add(new BusinessModelCommands.AutofacDiModule());

            foreach (var module in modulesToLoad)
            {
                containerBuilder.RegisterModule(module);
            }

            appContainer = containerBuilder.Build();

            return new AutofacServiceProvider(appContainer);
        }

        public static IServiceCollection ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("PortfolioBackEndDb");
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<PortfolioOperationsDbContext>(options => options.UseSqlServer(connection))
                //TODO: replace readonly dbContext by redis cache
                .AddDbContext<PortfolioReadOnlyDbContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
