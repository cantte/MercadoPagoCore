using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MercadoPagoCore.Http
{
    public interface IHttpClient
    {
        Task<MercadoPagoResponse> SendAsync(
            MercadoPagoRequest request,
            IRetryStrategy retryStrategy,
            CancellationToken cancellationToken);
    }
}
