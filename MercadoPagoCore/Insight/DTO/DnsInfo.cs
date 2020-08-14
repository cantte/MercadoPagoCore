using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class DnsInfo
    {
        [JsonProperty("nameserver-address")]
        public string NameServerAddress { get; set; }

        [JsonProperty("total-lookup-time-millis")]
        public long LookupTime { get; set; }
    }
}
