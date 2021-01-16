using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace MercadoPagoCore.Http
{
    public class DefaultRetryStrategy : IRetryStrategy
    {
        public static TimeSpan MinDelay => TimeSpan.FromMilliseconds(500);
        public static TimeSpan MaxDelay => TimeSpan.FromSeconds(5);

        public DefaultRetryStrategy(int maxNumberRetries)
        {
            MaxNumberRetries = maxNumberRetries;
        }

        public int MaxNumberRetries { get; }

        public RetryResponse ShouldRetry(HttpRequestMessage httpRequest, HttpResponseMessage httpResponse, bool hadRetryableError,
            int numberRetries)
        {
            bool retry = IsRequestRetryable(httpRequest) && numberRetries < MaxNumberRetries &&
                         (hadRetryableError || IsResponseRetryable(httpResponse));

            TimeSpan delay = TimeSpan.Zero;
            if (retry)
            {
                delay = ExponentialBackoffDelay(numberRetries);
            }

            return new RetryResponse(retry, delay);
        }

        private static bool IsRequestRetryable(HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
            {
                return false;
            }

            return httpRequest.Method != System.Net.Http.HttpMethod.Post
                   || (httpRequest.Headers.TryGetValues(Headers.IdempotencyKey, out IEnumerable<string> values)
                       && !string.IsNullOrWhiteSpace(string.Join("", values)));
        }

        private static bool IsResponseRetryable(HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                return false;
            }

            return httpResponse.StatusCode == HttpStatusCode.Conflict
                   || (int)httpResponse.StatusCode >= (int)HttpStatusCode.InternalServerError;
        }

        private static TimeSpan ExponentialBackoffDelay(int numberRetries)
        {
            var delay = TimeSpan.FromTicks(
                (long)(MinDelay.Ticks * Math.Pow(2, numberRetries)));

            if (delay > MaxDelay)
            {
                delay = MaxDelay;
            }

            return delay;
        }
    }
}
