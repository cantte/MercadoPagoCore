using System;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class Refund : MercadoPagoBase
    {
        public decimal? Id { get; private set; }
        public decimal? Amount { get; set; }
        public decimal? PaymentId { get; private set; }
        public DateTime? DateCreated
        {
            get; private set;
        }

        public void ManualSetPaymentId(decimal id)
        {
            PaymentId = id;
        }

        public Refund Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/v1/payments/:payment_id/refunds")]
        public Refund Save(RequestOptions requestOptions)
        {
            return (Refund)ProcessMethod<Refund>("Save", WITHOUT_CACHE, requestOptions);
        }
    }
}
