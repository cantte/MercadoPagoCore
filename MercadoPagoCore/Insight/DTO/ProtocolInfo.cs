using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class ProtocolInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("protocol-http")]
        public ProtocolHttp ProtocolHttp { get; set; }

        [JsonProperty("retry-count")]
        public int RetryCount { get; set; }
    }
}
