using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class ClientInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
