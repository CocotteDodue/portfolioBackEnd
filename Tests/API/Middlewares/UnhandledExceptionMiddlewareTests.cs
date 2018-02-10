using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Newtonsoft.Json;
using Portfolio.Api.ApiResult;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Api.Middlewares
{
    public class UnhandledExceptionMiddlewareTests
    {
        [Fact]
        public async Task UnhandledExceptionMiddleware_MustFormatPortfolioApiResultResponse_FromUnhandledException()
        {
            // Assign
            var fakeEnvironment = new Mock<IHostingEnvironment>();
            fakeEnvironment
                .Setup(env => env.EnvironmentName)
                .Returns("plopPlop");

            Exception exception = new Exception("UnitTest Exception");
            var fakeExceptionHandlerFeature = new Mock<IExceptionHandlerFeature>();
            fakeExceptionHandlerFeature
                .Setup(e => e.Error)
                .Returns(exception);

            var fakeFeatureCollection = new Mock<IFeatureCollection>();
            fakeFeatureCollection
                .Setup(features => features.Get<IExceptionHandlerFeature>())
                .Returns(fakeExceptionHandlerFeature.Object);

            var fakeHttpContext = new Mock<HttpContext>();
            fakeHttpContext
                .Setup(context => context.Features)
                .Returns(fakeFeatureCollection.Object);

            HttpResponse response = new DefaultHttpContext().Response;
            response.Body = new MemoryStream();

            fakeHttpContext
                .Setup(context => context.Response)
                .Returns(response);

            var exceptionMiddleware = new UnhandledExceptionMiddleware(fakeEnvironment.Object);

            // Act
            await exceptionMiddleware.Invoke(fakeHttpContext.Object);

            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyRed = new StreamReader(response.Body).ReadToEnd();

            PortfolioApiResult writenResponseResult = JsonConvert.DeserializeObject<PortfolioApiResult>(responseBodyRed);

            // Assert
            // TODO: implement serserializer 
            Assert.NotNull(writenResponseResult);
        }
    }
}
