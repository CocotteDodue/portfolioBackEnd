using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Portfolio.Api.ApiResult
{
    public class PortfolioApiResultJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override bool CanRead => true;
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            PortfolioApiResult result = (PortfolioApiResult)value;

            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            if (value != null)
            {
                Type apiResultType = result.GetType();
                writer.WriteStartObject();
                SerializePropertyFromApiResult(nameof(PortfolioApiResult.Success), writer, serializer, result, apiResultType);
                string propertyNameToSerialize = GetPropertyNameToSerialize(result);
                SerializePropertyFromApiResult(propertyNameToSerialize, writer, serializer, result, apiResultType);

                writer.WriteEndObject();
            }
        }

        private static string GetPropertyNameToSerialize(PortfolioApiResult result)
        {
            return !result.Success && result.Error != null
                ? nameof(PortfolioApiResult.Error)
                : nameof(PortfolioApiResult.Result);
        }

        private void SerializePropertyFromApiResult(string propertyName, JsonWriter writer, JsonSerializer serializer, PortfolioApiResult resultObjectToSerialize, Type apiResultType)
        {
            var resultPropertyToSerialize = apiResultType.GetProperty(propertyName);

            writer.WritePropertyName(ResolveUsingCamelCasing(resultPropertyToSerialize.Name));
            serializer.Serialize(writer, resultPropertyToSerialize.GetValue(resultObjectToSerialize));
        }

        private string ResolveUsingCamelCasing(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            return name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);
        }
    }
}
