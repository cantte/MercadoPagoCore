namespace MercadoPagoCore.Core
{
    public class GETEndpoint : BaseEndpoint
    {
        public GETEndpoint(string path) : base(path, HttpMethod.GET, 0, 0)
        {
        }

        public GETEndpoint(string path, int requestTimeout) : base(path, HttpMethod.GET, requestTimeout, 0)
        {
        }

        public GETEndpoint(string path, int requestTimeout, int retries) : base(path, HttpMethod.GET, requestTimeout, retries)
        {
        }
    }
}
