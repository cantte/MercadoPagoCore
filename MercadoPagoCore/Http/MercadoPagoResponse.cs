using System.Collections.Generic;

namespace MercadoPagoCore.Http
{
    public class MercadoPagoResponse
    {
        public MercadoPagoResponse(int statusCode, IDictionary<string, string> headers, string content)
        {
            StatusCode = statusCode;
            Headers = headers;
            Content = content;
        }

        public int StatusCode { get; }
        public IDictionary<string, string> Headers { get; }
        public string Content { get; }
    }
}
