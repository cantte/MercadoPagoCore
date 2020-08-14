using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class ProtocolHttp
    {
        [JsonProperty("referer-url")]
        public string RefererUrl { get; set; }

        [JsonProperty("request-method")]
        public string RequestMethod { get; set; }

        [JsonProperty("request-url")]
        public string RequestUrl { get; set; }

        [JsonProperty("request-headers")]
        public IDictionary<string, string> RequestHeaders { get; private set; }

        [JsonProperty("response-status-code")]
        public int ResponseCode { get; set; }

        [JsonProperty("response-headers")]
        public IDictionary<string, string> ResponseHeaders { get; private set; }

        [JsonProperty("first-byte-time-millis")]
        public long FirstByteTime { get; set; }

        [JsonProperty("last-byte-time-millis")]
        public long LastByteTime { get; set; }

        [JsonProperty("was-cached")]
        public bool WasCached { get; set; }

        public ProtocolHttp()
        {
            this.RequestHeaders = new Dictionary<string, string>();
            this.ResponseHeaders = new Dictionary<string, string>();
        }

        public void AddRequestHeader(string name, string value)
        {
            this.RequestHeaders.Add(name, value);
        }

        public void AddResponseHeader(string name, string value)
        {
            this.ResponseHeaders.Add(name, value);
        }
    }
}
