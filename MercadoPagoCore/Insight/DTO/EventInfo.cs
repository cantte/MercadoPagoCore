using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class EventInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
