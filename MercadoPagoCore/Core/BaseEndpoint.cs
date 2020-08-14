using System;
using MercadoPagoCore.Core.Annotations;

namespace MercadoPagoCore.Core
{
    public class BaseEndpoint : Attribute
    {
        public BaseEndpoint(string path, HttpMethod httpMethod, int requestTimeout, int retries)
        {
            Path = path;
            HttpMethod = httpMethod;
            RequestTimeout = requestTimeout;
            Retries = (retries == 0) ? 1 : retries;
            PayloadType = PayloadType.JSON;
        }

        public string Path { get; protected set; }
        public HttpMethod HttpMethod { get; set; }
        public PayloadType PayloadType { get; set; }
        public int Retries { get; set; }
        public int RequestTimeout { get; protected set; }
    }
}
