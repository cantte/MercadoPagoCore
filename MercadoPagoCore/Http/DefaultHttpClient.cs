using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MercadoPagoCore.Http
{
    public class DefaultHttpClient : IHttpClient
    {
        private const string JsonContentType = "application/json";

        private static readonly Lazy<HttpClient> LAZY_DEFAULT_HTTP_CLIENT = new(() => new HttpClient
        {
            Timeout = DefaultHttpTimeout
        });

        public static TimeSpan DefaultHttpTimeout => TimeSpan.FromSeconds(30);

        static DefaultHttpClient()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }

        public DefaultHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public DefaultHttpClient() : this(LAZY_DEFAULT_HTTP_CLIENT.Value)
        {
        }

        public HttpClient HttpClient { get; }

        public async Task<MercadoPagoResponse> SendAsync(MercadoPagoRequest request, IRetryStrategy retryStrategy, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponse = null;
            int numberRetries = 0;
            Exception exception;
            HttpRequestMessage httpRequest;

            while (true)
            {
                httpRequest = CreateHttpRequest(request);

                try
                {
                    httpResponse = await HttpClient
                        .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false);
                    exception = null;
                }
                catch (Exception e)
                {
                    exception = e;
                }

                RetryResponse retryResponse = retryStrategy.ShouldRetry(httpRequest, httpResponse,
                    IsRetryableError(exception, cancellationToken), numberRetries);

                if (!retryResponse.Retry)
                {
                    break;
                }

                httpResponse?.Dispose();

                ++numberRetries;
                await Task.Delay(retryResponse.Delay, cancellationToken).ConfigureAwait(false);
            }

            httpRequest?.Dispose();

            if (exception is not null)
            {
                throw exception;
            }

            return await MapResponse(httpResponse);
        }

        private static HttpRequestMessage CreateHttpRequest(MercadoPagoRequest request)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = MapHttpMethod(request.Method),
                RequestUri = new Uri(request.Url),
            };

            if (request.Content != null)
            {
                httpRequest.Content = new StringContent(request.Content, Encoding.UTF8, JsonContentType);
            }

            if (request.Headers == null)
            {
                return httpRequest;
            }

            foreach (var (key, value) in request.Headers)
            {
                httpRequest.Headers.Add(key, value);
            }
            return httpRequest;
        }

        private static async Task<MercadoPagoResponse> MapResponse(HttpResponseMessage httpResponse)
        {
            string content;
            Stream stream = await httpResponse.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);
            using (var sr = new StreamReader(stream))
            {
                content = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var headers = httpResponse.Headers.ToDictionary(httpHeader => httpHeader.Key, httpHeader => string.Join(",", httpHeader.Value));

            // Disposes HTTP response
            httpResponse.Dispose();

            return new MercadoPagoResponse(
                (int)httpResponse.StatusCode,
                headers,
                content);
        }

        private static bool IsRetryableError(Exception exception,
            CancellationToken cancellationToken) =>
            exception != null
            && (exception is HttpRequestException
                || exception is OperationCanceledException
                    && !cancellationToken.IsCancellationRequested);

        private static System.Net.Http.HttpMethod MapHttpMethod(HttpMethod httpMethod)
        {
            switch (httpMethod)
            {
                case HttpMethod.Get:
                    return System.Net.Http.HttpMethod.Get;
                case HttpMethod.Post:
                    return System.Net.Http.HttpMethod.Post;
                case HttpMethod.Put:
                    return System.Net.Http.HttpMethod.Put;
                case HttpMethod.Delete:
                    return System.Net.Http.HttpMethod.Delete;
                default:
                    throw new ArgumentException($"Invalid value of {nameof(httpMethod)}.");
            }
        }
    }
}
