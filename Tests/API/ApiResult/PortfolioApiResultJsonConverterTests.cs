using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portfolio.Contracts.Common;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Portfolio.Api.ApiResult
{
    public class PortfolioApiResultJsonConverterTests
    {
        [Fact]
        public void ApiResultConverter_CanNeverConvert()
        {
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();

            bool canConvert = converter.CanConvert(typeof(object));
            canConvert |= converter.CanConvert(typeof(PortfolioApiResult));

            Assert.False(canConvert);
        }

        [Fact]

        public void ApiResultConverter_ThrowsNotImplementedExceotion_WhenReadJsonCalled()
        {
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();

            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => converter.ReadJson(null, null, null, null));
        }

        [Fact]
        public void ApiResultConverter_MustUseCamelCasing_WhenWritingTopLevelProperties()
        {
            PortfolioApiResult result = new PortfolioApiResult("Test");
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, result, serializer);
                JObject resultObject = JsonConvert.DeserializeObject<JObject>(sb.ToString());
                var properties = resultObject.Children();

                foreach (var property in properties)
                {
                    Assert.True(char.IsLower(property.Path.First()));
                }
            }
        }

        [Fact]
        public void ApiResultConverter_MustWriteSuccessProperty_WhenWritingRootLevelProperties()
        {
            PortfolioApiResult result = new PortfolioApiResult("Test");
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, result, serializer);
                JObject resultObject = JsonConvert.DeserializeObject<JObject>(sb.ToString());
                var properties = resultObject.Children();

                var successProperty = properties.FirstOrDefault(p => p.Path.StartsWith(nameof(PortfolioApiResult.Success), StringComparison.InvariantCultureIgnoreCase));
                Assert.NotNull(successProperty);
            }
        }

        [Fact]
        public void ApiResultConverter_MustWriteResultProperty_WhenWritingRootLevelPropertiesFromNonErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult("Test");
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, result, serializer);
                JObject resultObject = JsonConvert.DeserializeObject<JObject>(sb.ToString());
                var properties = resultObject.Children();

                var resultProperty = properties.FirstOrDefault(p => p.Path.StartsWith(nameof(PortfolioApiResult.Result), StringComparison.InvariantCultureIgnoreCase));
                Assert.NotNull(resultProperty);
            }
        }

        [Fact]
        public void ApiResultConverter_MustWriteResultProperty_WhenWritingRootLevelPropertiesFromErrorObject()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error() { Message = "Test Error" });
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, result, serializer);
                JObject resultObject = JsonConvert.DeserializeObject<JObject>(sb.ToString());
                var properties = resultObject.Children();

                var errorProperty = properties.FirstOrDefault(p => p.Path.StartsWith(nameof(PortfolioApiResult.Error), StringComparison.InvariantCultureIgnoreCase));
                Assert.NotNull(errorProperty);
            }
        }
        [Fact]
        public void ApiResultConverter_MustUseCamelCasing_WhenWritingChildLevelProperties()
        {
            PortfolioApiResult result = new PortfolioApiResult(new Error() { Message = "Test Error" });
            PortfolioApiResultJsonConverter converter = new PortfolioApiResultJsonConverter();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, result, serializer);
                JObject resultObject = JsonConvert.DeserializeObject<JObject>(sb.ToString());
                var properties = resultObject.Children();
                var errorProperty = properties.FirstOrDefault(p => p.Path.StartsWith(nameof(PortfolioApiResult.Error), StringComparison.InvariantCultureIgnoreCase));

                var messageProperty = errorProperty.Children().Children().FirstOrDefault(p => p.Path.IndexOf(nameof(Error.Message), StringComparison.InvariantCultureIgnoreCase) > -1);
                Assert.True(char.IsLower(messageProperty.Path.First()));
            }
        }
    }
}
