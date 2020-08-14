using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class TrafficLightRequest
    {
        [JsonProperty("client-info")]
        public ClientInfo ClientInfo { get; set; }
    }
}
