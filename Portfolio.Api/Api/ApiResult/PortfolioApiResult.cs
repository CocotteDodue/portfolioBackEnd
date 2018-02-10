using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolio.Contracts.Common;
using System.Net;
using System.Threading.Tasks;

namespace Portfolio.Api.ApiResult
{
    [JsonConverter(typeof(PortfolioApiResultJsonConverter))]
    public class PortfolioApiResult : JsonResult
    {
        public bool Success { get; set; }

        public object Result => Value;

        public Error Error => Value is Error ? (Error)Value : null;

        public int CustomStatusCode => StatusCode.Value;

        public PortfolioApiResult(object value, HttpStatusCode statusCode)
            : base(value)
        {
            StatusCode = (int)statusCode;
            GetSuccessValueFromResultType();
            ContentType = "application/json";
        }

        public PortfolioApiResult(object value) 
            : this(value, HttpStatusCode.OK)
        { }

        public PortfolioApiResult(object value, HttpStatusCode statusCode, bool success) 
            : this(value, statusCode)
        {
            Success = success;
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;

            response.ContentType = ContentType;
            response.StatusCode = CustomStatusCode;

            var serializedJsonResult = JsonConvert.SerializeObject(this);

            await response.WriteAsync(serializedJsonResult);
        }

        private void GetSuccessValueFromResultType()
        {
            Success = !(Value is Error);
        }
    }
}
