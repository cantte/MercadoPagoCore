namespace MercadoPagoCore.Core
{
    public class DELETEEndpoint : BaseEndpoint
    {
        public DELETEEndpoint(string path) : base(path, HttpMethod.DELETE, 0, 0)
        {
        }

        public DELETEEndpoint(string path, int requestTimeout) : base(path, HttpMethod.DELETE, requestTimeout, 0)
        {
        }

        public DELETEEndpoint(string path, int requestTimeout, int retries) : base(path, HttpMethod.DELETE, requestTimeout, retries)
        {
        }
    }
}
