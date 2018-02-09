﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Api.DiRegistration;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Portfolio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer AppContainer;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabases(Configuration);

            services.AddMvc();

            // Swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Portfolio API",
                    Version = "v1",
                    Description = "ASP.NET Core Web API Application supporting my online Portfolio, along with bloging and my 2 cents on techs and how to.",
                    Contact = new Contact { Name = "Mathilde Ceccaroli", Email = "", Url = "tbd" },
                    License = new License { Name = "Developed and distributed under the MIT License", Url = "https://opensource.org/licenses/MIT" }
                });
            });
            
            return services.RegisterAutofacIoCContainer( out AppContainer);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureSwagger(app, env);

            app.UseMvc();

            // properly dispose of the IoC AppContainer when the request is done
            appLifetime.ApplicationStopped.Register(() => AppContainer.Dispose());
        }

        private void ConfigureSwagger(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1");
            });
        }

       }
}