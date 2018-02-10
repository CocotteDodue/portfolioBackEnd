using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Api.DiRegistration;
using Portfolio.Api.Middlewares;
using Portfolio.Common.DeveloperTools;
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
            services.AddCors(options =>
                options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                })
            );

            services.ConfigureDatabases(Configuration);

            services.AddMvc();

            services.ConfigureSwagger();
            
            return services.RegisterAutofacIoCContainer( out AppContainer);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            app.UseCors("AllowAllOrigins");

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new UnhandledExceptionMiddleware(env).Invoke
            });

            app.AddSwaggerToApi(env);

            app.UseMvc();

            // properly dispose of the IoC AppContainer when the request is done
            appLifetime.ApplicationStopped.Register(() => AppContainer.Dispose());
        }

       }
}
