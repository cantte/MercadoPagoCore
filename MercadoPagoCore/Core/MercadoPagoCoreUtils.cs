using System;
using System.Collections.Generic;
using MercadoPagoCore.DataStructures.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MercadoPagoCore.Core
{
    public static class MercadoPagoCoreUtils
    {
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }

        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }

        public static JObject GetJsonFromResource<T>(T resource) where T : MercadoPagoBase
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomSerializationContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            serializer.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK"
            });

            JObject jobject = JObject.FromObject(resource, serializer);

            return jobject;
        }

        public static MercadoPagoBase GetResourceFromJson<T>(Type type, JObject jObj) where T : MercadoPagoBase
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomDeserializationContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            serializer.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK"
            });

            T resource = jObj.ToObject<T>(serializer);

            resource.DumpLog();

            return resource;
        }

        public static BadParamsError GetBadParamsError(string raw)
        {
            JObject jObj = JObject.Parse(raw);

            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomDeserializationContractResolver()
            };

            BadParamsError badParams = jObj.ToObject<BadParamsError>(serializer);
            return badParams;
        }



        public static JArray GetArrayFromJsonElement<T>(JObject jsonElement) where T : MercadoPagoBase
        {
            return GetJArrayFromStringResponse<T>(jsonElement["results"].ToString());
        }

        public static JArray GetJArrayFromStringResponse<T>(string stringResponse) where T : MercadoPagoBase
        {
            return JArray.Parse(stringResponse);
        }
    }
}
