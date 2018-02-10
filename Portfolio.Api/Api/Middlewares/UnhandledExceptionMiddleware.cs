using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Portfolio.Api.ApiResult;
using Portfolio.Common.Extensions;
using Portfolio.Contracts.Common;
using System.Net;
using System.Threading.Tasks;

namespace Portfolio.Api.Middlewares
{
    /// <summary>
    /// Middleware registered with UseExceptionHandler method in Configure Method
    /// In effect only when unhandled exception are going out of the pipeline after last result filter executed
    /// </summary>
    public class UnhandledExceptionMiddleware
    {
        private IHostingEnvironment _env;

        public UnhandledExceptionMiddleware(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Invoked only when a non-handled exception is in the context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>();
            Error error = PrepareErrorObject(ex);

            PortfolioApiResult result = new PortfolioApiResult(error, HttpStatusCode.InternalServerError);
            context.Response.StatusCode = result.CustomStatusCode;
            context.Response.ContentType = result.ContentType;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private Error PrepareErrorObject(IExceptionHandlerFeature exception)
        {
            Error error = new Error();

            if (exception != null)
            {
                error.Message = _env.IsAnyProductionEnv()
                    ? "Server's on fire bro!"
                    : ExctractMessage(exception);

                if (!_env.IsAnyProductionEnv())
                {
                    error.AdditionalInfos = new { StackTrace = exception.Error.StackTrace };
                }
            }

            return error;
        }

        private string ExctractMessage(IExceptionHandlerFeature exception)
        {
            return exception.Error.InnerException?.Message ?? exception.Error.Message;
        }
    }
}
