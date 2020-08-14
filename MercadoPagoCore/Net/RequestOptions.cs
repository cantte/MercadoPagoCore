using System.Collections.Generic;
using System.Net;

namespace MercadoPagoCore.Net
{
    public class RequestOptions
    {
        public string AccessToken { get; set; }

        public int Timeout { get; set; }

        public int Retries { get; set; }

        public IWebProxy Proxy { get; set; }

        public IDictionary<string, string> CustomHeaders { get; set; }

        public IDictionary<string, string> TrackHeaders { get; private set; }

        public RequestOptions()
        {
            Timeout = MercadoPagoSDK.RequestsTimeout;
            Retries = MercadoPagoSDK.RequestsRetries;
            Proxy = MercadoPagoSDK.Proxy;
            CustomHeaders = new Dictionary<string, string>();
            TrackHeaders = new Dictionary<string, string> {
                { "x-platform-id", MercadoPagoSDK.PlatformId },
                { "x-corporation-id", MercadoPagoSDK.CorporationId },
                { "x-integrator-id", MercadoPagoSDK.IntegratorId }
            };
        }
    }
}
