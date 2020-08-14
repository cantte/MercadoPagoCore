using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class ConnectionInfo
    {
        [JsonProperty("network-type")]
        public string NetworkType { get; set; }

        [JsonProperty("network-speed")]
        public string NetworkSpeed { get; set; }

        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }

        [JsonProperty("was-reused")]
        public bool WasReused { get; set; }

        [JsonProperty("dns-info")]
        public DnsInfo DnsInfo { get; set; }

        [JsonProperty("certificate-info")]
        public CertificateInfo CertificateInfo { get; set; }

        [JsonProperty("tcp-info")]
        public TcpInfo TcpInfo { get; set; }

        [JsonProperty("protocol-info")]
        public ProtocolInfo ProtocolInfo { get; set; }

        [JsonProperty("is-complete")]
        public bool IsDataComplete { get; set; }
    }
}
