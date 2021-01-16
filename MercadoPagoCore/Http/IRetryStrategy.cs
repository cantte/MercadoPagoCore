using System.Net.Http;

namespace MercadoPagoCore.Http
{
    public interface IRetryStrategy
    {
        RetryResponse ShouldRetry(
            HttpRequestMessage httpRequest,
            HttpResponseMessage httpResponse,
            bool hadRetryableError,
            int numberRetries);
    }
}
