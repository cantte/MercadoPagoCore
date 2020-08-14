using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class TrafficLightResponse
    {
        [JsonProperty("send-data")]
        public bool IsSendDataEnabled { get; set; }

        [JsonProperty("ttl")]
        public int SendTtl { get; set; }

        [JsonProperty("endpoint-whitelist")]
        public List<string> EndpointWhitelist { get; set; }

        [JsonProperty("base64-encode-data")]
        public bool IsBase64EncodingEnabled { get; set; }

        public bool IsEndpointInWhiteList(string requestUrl)
        {
            if (this.EndpointWhitelist == null)
            {
                return false;
            }

            foreach (string pattern in EndpointWhitelist)
            {
                if (pattern.Equals("*"))
                {
                    return true;
                }

                bool matched = true;
                string[] parts = pattern.Split('*');
                foreach (string part in parts)
                {
                    if (part.Length == 0)
                    {
                        continue;
                    }
                    matched = matched && requestUrl.ToLower().Contains(part);
                }

                if (matched)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
