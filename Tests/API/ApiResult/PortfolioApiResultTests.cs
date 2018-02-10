using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Portfolio.Contracts.Common;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Api.ApiResult
{
    public class PortfolioApiResultTests
    {
        #region single parameter contructor behavior
        [Fact]
        public void ApiResult_HasApplicationJsonContentType_WhenCreatedOnlyFromSingleParameter()
        {
            PortfolioApiResult result = new PortfolioApiResult(new object());

            Assert.Equal("application/json", result.ContentType);
        }

        [Fact]
        public void ApiResult_ResultFieldContainsValue_WhenCreatedOnlyFromNonErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue");

            Assert.Equal("TestValue", result.Result);
        }

        [Fact]
        public void ApiResult_ErrorFieldMustBeNull_WhenCreatedOnlyFromNonErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue");

            Assert.Null(result.Error);
        }

        [Fact]
        public void ApiResult_ResultAndErrorFieldsMustBeEqualsAndContainsError_WhenCreatedOnlyFromErrorObject()
        {
            var error = new Error();
            PortfolioApiResult result = new PortfolioApiResult(error);

            Assert.Equal(error, result.Error);
            Assert.Equal(error, result.Result);
        }

        [Fact]
        public void ApiResult_HasOkStatusCode_WhenCreatedOnlyFromNonErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue");

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasOkStatusCode_WhenCreatedOnlyFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error());

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasValidSuccess_WhenCreatedOnlyFromNonErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue");

            Assert.True(result.Success);
        }

        [Fact]
        public void ApiResult_HasInvalidSuccess_WhenCreatedOnlyFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error());

            Assert.False(result.Success);
        }
        #endregion

        #region 2 parameters constructor: Result + status code
        [Fact]
        public void ApiResult_HasApplicationJsonContentType_WhenCreatedFromResultAndStatucCode()
        {
            PortfolioApiResult result = new PortfolioApiResult(new object(), HttpStatusCode.BadGateway);

            Assert.Equal("application/json", result.ContentType);
        }

        [Fact]
        public void ApiResult_ResultFieldContainsValue_WhenCreatedFromNonErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway);

            Assert.Equal("TestValue", result.Result);
        }

        [Fact]
        public void ApiResult_ErrorFieldMustBeNull_WhenCreatedFromNonErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway);

            Assert.Null(result.Error);
        }

        [Fact]
        public void ApiResult_ResultAndErrorFieldsMustBeEqualsAndContainsError_WhenCreatedFromErrorObjectAndStatusCode()
        {
            var error = new Error();
            PortfolioApiResult result = new PortfolioApiResult(error, HttpStatusCode.BadGateway);

            Assert.Equal(error, result.Error);
            Assert.Equal(error, result.Result);
        }

        [Fact]
        public void ApiResult_HasStatusCodeFromConstructor_WhenCreatedFromNonErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway);

            Assert.Equal((int)HttpStatusCode.BadGateway, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasStatusCodeFromConstructor_WhenCreatedFromErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error(), HttpStatusCode.BadGateway);

            Assert.Equal((int)HttpStatusCode.BadGateway, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasValidSuccess_WhenCreatedFromNonErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway);

            Assert.True(result.Success);
        }

        [Fact]
        public void ApiResult_HasInvalidSuccess_WhenCreatedFromErrorObjectAndStatusCode()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error(), HttpStatusCode.BadGateway);

            Assert.False(result.Success);
        }
        #endregion
        
        #region 3 parameters constructor: Result + status code + success
        [Fact]
        public void ApiResult_HasApplicationJsonContentType_WhenCreatedFromResultAndStatucCodeAndSuccess()
        {
            PortfolioApiResult result = new PortfolioApiResult(new object(), HttpStatusCode.BadGateway, true);

            Assert.Equal("application/json", result.ContentType);
        }

        [Fact]
        public void ApiResult_ResultFieldContainsValue_WhenCreatedFromNonErrorObjectAndStatusCodeAndSuccess()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway, true);

            Assert.Equal("TestValue", result.Result);
        }

        [Fact]
        public void ApiResult_ErrorFieldMustBeNull_WhenCreatedFromNonErrorObjectAndStatusCodeAndSuccess()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway, true);

            Assert.Null(result.Error);
        }

        [Fact]
        public void ApiResult_ResultAndErrorFieldsMustBeEqualsAndContainsError_WhenCreatedFromErrorObjectAndStatusCodeAndSuccess()
        {
            var error = new Error();
            PortfolioApiResult result = new PortfolioApiResult(error, HttpStatusCode.BadGateway, true);

            Assert.Equal(error, result.Error);
            Assert.Equal(error, result.Result);
        }

        [Fact]
        public void ApiResult_HasStatusCodeFromConstructor_WhenCreatedFromNonErrorObjectAndStatusCodeAndSuccess()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway, true);

            Assert.Equal((int)HttpStatusCode.BadGateway, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasStatusCodeFromConstructor_WhenCreatedFromErrorObjectAndStatusCodeAndSuccess()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error(), HttpStatusCode.BadGateway, true);

            Assert.Equal((int)HttpStatusCode.BadGateway, result.StatusCode.Value);
        }

        [Fact]
        public void ApiResult_HasValidSuccess_WhenCreatedwithValidSuccessandNotFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway, true);

            Assert.True(result.Success);
        }

        [Fact]
        public void ApiResult_HasValidSuccess_WhenCreatedwithValidSuccessandFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error(), HttpStatusCode.BadGateway, true);

            Assert.True(result.Success);
        }

        [Fact]
        public void ApiResult_HasInvalidSuccess_WhenCreatedwithinvalidSuccessandNotFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("TestValue", HttpStatusCode.BadGateway, false);

            Assert.False(result.Success);
        }

        [Fact]
        public void ApiResult_HasInvalidSuccess_WhenCreatedwithInvalidSuccessandFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error(), HttpStatusCode.BadGateway, false);

            Assert.False(result.Success);
        }
        #endregion

        [Fact]
        public async Task ApiResult_MustSetHttpResponseContentType_WhenExecutingResultAsync()
        {
            var defaultHttpContext = new DefaultHttpContext();
            var dummyActionContext = new ActionContext(defaultHttpContext, new RouteData(), new ActionDescriptor());

            var result = new PortfolioApiResult("Test");
            await result.ExecuteResultAsync(dummyActionContext);

            Assert.Equal(result.ContentType, dummyActionContext.HttpContext.Response.ContentType);
        }

        [Fact]
        public async Task ApiResult_MustSetHttpResponseStatusCode_WhenExecutingResultAsync()
        {
            var defaultHttpContext = new DefaultHttpContext();
            var dummyActionContext = new ActionContext(defaultHttpContext, new RouteData(), new ActionDescriptor());

            var result = new PortfolioApiResult("Test", HttpStatusCode.Ambiguous);
            await result.ExecuteResultAsync(dummyActionContext);

            Assert.Equal((int)HttpStatusCode.Ambiguous, dummyActionContext.HttpContext.Response.StatusCode);
        }

        [Fact]
        public async Task ApiResult_MustWriteResultToHttpResponseBody_WhenExecutingResultAsync()
        {
            var defaultHttpContext = new DefaultHttpContext();
            defaultHttpContext.Response.Body = new MemoryStream();
            var dummyActionContext = new ActionContext(defaultHttpContext, new RouteData(), new ActionDescriptor());

            var result = new PortfolioApiResult("Test", HttpStatusCode.Ambiguous);
            await result.ExecuteResultAsync(dummyActionContext);
            dummyActionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            string responseBodyRed = new StreamReader(dummyActionContext.HttpContext.Response.Body).ReadToEnd();
            Assert.True(!string.IsNullOrWhiteSpace(responseBodyRed));
        }
    }
}
