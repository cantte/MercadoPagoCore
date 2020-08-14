namespace MercadoPagoCore.Core
{
    public class POSTEndpoint : BaseEndpoint
    {
        public POSTEndpoint(string path) : base(path, HttpMethod.POST, 0, 0)
        {
        }

        public POSTEndpoint(string path, int requestTimeout) : base(path, HttpMethod.POST, requestTimeout, 0)
        {
        }

        public POSTEndpoint(string path, int requestTimeout, int retries) : base(path, HttpMethod.POST, requestTimeout, retries)
        {
        }
    }
}
