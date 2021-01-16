using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MercadoPagoCore.Serialization
{
    public class DefaultSerializer : ISerializer
    {
        private const string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        public DefaultSerializer()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                DateFormatString = DateFormatString,
                Culture = CultureInfo.InvariantCulture,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            JsonSerializer = JsonSerializer.Create(JsonSerializerSettings);
        }

        public JsonSerializerSettings JsonSerializerSettings { get; }
        public JsonSerializer JsonSerializer { get; }

        public TResponse DeserializeFromJson<TResponse>(string json) where TResponse : new()
        {
            return JsonConvert.DeserializeObject<TResponse>(json, JsonSerializerSettings);
        }

        public string SerializeToJson(object request)
        {
            return JsonConvert.SerializeObject(request, JsonSerializerSettings);
        }

        public Task<string> SerializeToQueryStringAsync(object request)
        {
            IDictionary<string, string> dictionary = ParseToDictionary(request);
            if (dictionary == null)
            {
                return Task.FromResult(string.Empty);
            }

            FormUrlEncodedContent urlEncoded = new FormUrlEncodedContent(dictionary);
            return urlEncoded.ReadAsStringAsync();
        }

        private IDictionary<string, string> ParseToDictionary(object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                JObject jObject = JObject.FromObject(metaToken, JsonSerializer);
                return ParseToDictionary(jObject);
            }

            if (token.HasValues)
            {
                Dictionary<string, string> contentData = new Dictionary<string, string>();
                foreach (JToken child in token.Children().ToList())
                {
                    IDictionary<string, string> childContent = ParseToDictionary(child);
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            JValue jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            string value = jValue.Type == JTokenType.Date ?
                jValue.ToString(DateFormatString, CultureInfo.InvariantCulture) :
                jValue.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
    }
}
