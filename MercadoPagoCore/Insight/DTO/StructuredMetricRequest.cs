using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class StructuredMetricRequest
    {
        [JsonProperty("client-info")]
        public ClientInfo ClientInfo { get; set; }

        [JsonProperty("business-flow-info")]
        public BusinessFlowInfo BusinessFlowInfo { get; set; }

        [JsonProperty("event-info")]
        public EventInfo EventInfo { get; set; }

        [JsonProperty("connection-info")]
        public ConnectionInfo ConnectionInfo { get; set; }

        [JsonProperty("device-info")]
        public DeviceInfo DeviceInfo { get; set; }

        [JsonProperty("encoded-data")]
        public string Base64Data { get; set; }
    }
}
