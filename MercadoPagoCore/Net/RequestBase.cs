using System.Net;

namespace MercadoPagoCore.Net
{
    public class RequestBase
    {
        public HttpWebRequest Request { get; set; }
        public byte[] RequestPayload { get; set; }
    }
}
