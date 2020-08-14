using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class TcpInfo
    {
        [JsonProperty("source-address")]
        public string SourceAddress { get; set; }

        [JsonProperty("target-address")]
        public string TargetAddress { get; set; }

        [JsonProperty("handshake-time-millis")]
        public long HandshakeTime { get; set; }
    }
}
