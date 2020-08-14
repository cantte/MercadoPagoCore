namespace MercadoPagoCore.Core
{
    public class PUTEndpoint : BaseEndpoint
    {
        public PUTEndpoint(string path) : base(path, HttpMethod.PUT, 0, 0)
        {
        }

        public PUTEndpoint(string path, int requestTimeout) : base(path, HttpMethod.PUT, requestTimeout, 0)
        {
        }

        public PUTEndpoint(string path, int requestTimeout, int retries) : base(path, HttpMethod.PUT, requestTimeout, retries)
        {
        }
    }
}
