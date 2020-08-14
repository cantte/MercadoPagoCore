using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class DeviceInfo
    {
        [JsonProperty("os-name")]
        public string OsName { get; set; }

        [JsonProperty("model-name")]
        public string ModelName { get; set; }

        [JsonProperty("cpu-type")]
        public string CpuType { get; set; }

        [JsonProperty("ram-size")]
        public string RamSize { get; set; }
    }
}
