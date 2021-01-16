using System.Collections.Generic;

namespace MercadoPagoCore.Http
{
    public class MercadoPagoRequest
    {
        public MercadoPagoRequest(string url, HttpMethod method, IDictionary<string, string> headers, string content)
        {
            Url = url;
            Method = method;
            Headers = headers ?? new Dictionary<string, string>();
            Content = content;
        }

        public MercadoPagoRequest()
        {
            Method = HttpMethod.Get;
            Headers = new Dictionary<string, string>();
        }

        public string Url { get; set; }
        public HttpMethod Method { get; set; }
        public IDictionary<string, string> Headers { get; }
        public string Content { get; set; }
    }
}
