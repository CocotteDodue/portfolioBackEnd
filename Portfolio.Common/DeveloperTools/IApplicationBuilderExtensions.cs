using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Portfolio.Common.DeveloperTools
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwaggerToApi(this IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1");
            });

            return app;
        }
    }
}
