using System;
using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.Net;
using Newtonsoft.Json;

namespace MercadoPagoCore.Resources
{
    public class DisbursementRefund : MercadoPagoBase
    {
        public long? Id { get; set; }
        [JsonProperty("payment_id")]
        public long? DisbursementId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }

        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/refunds")]
        internal static List<DisbursementRefund> CreateAll(long advancedPaymentId, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "advanced_payment_id", advancedPaymentId.ToString() }
            };

            return ProcessMethodBulk<DisbursementRefund>(typeof(DisbursementRefund), "CreateAll", pathParams, WITHOUT_CACHE, requestOptions);
        }

        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/disbursements/:disbursement_id/refunds")]
        internal static DisbursementRefund Create(long advancedPaymentId, long disbursementId, decimal? amount, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "advanced_payment_id", advancedPaymentId.ToString() },
                { "disbursement_id", disbursementId.ToString() }
            };

            DisbursementRefund disbursementRefund = new DisbursementRefund
            {
                Amount = amount
            };

            return ProcessMethod(typeof(DisbursementRefund), disbursementRefund, "Create", pathParams, WITHOUT_CACHE, requestOptions);
        }
    }
}
