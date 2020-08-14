using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class BusinessFlowInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }
    }
}
