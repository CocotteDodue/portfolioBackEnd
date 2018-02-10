using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Portfolio.Common.DeveloperTools
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
