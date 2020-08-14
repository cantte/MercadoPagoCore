using Newtonsoft.Json;

namespace MercadoPagoCore.Insight.DTO
{
    public class CertificateInfo
    {
        [JsonProperty("certificate-type")]
        public string CertificateType { get; set; }

        [JsonProperty("certificate-version")]
        public string CertificateVersion { get; set; }

        [JsonProperty("certificate-expiration")]
        public string CertificateExpiration { get; set; }

        [JsonProperty("handshake-time-millis")]
        public long HandshakeTime { get; set; }
    }
}
