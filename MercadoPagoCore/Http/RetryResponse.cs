using System;

namespace MercadoPagoCore.Http
{
    public class RetryResponse
    {
        public RetryResponse(bool retry, TimeSpan delay)
        {
            Retry = retry;
            Delay = delay;
        }

        public bool Retry { get; set; }
        public TimeSpan Delay { get; set; }
    }
}
