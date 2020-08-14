using System;
using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.Net;
using Newtonsoft.Json;

namespace MercadoPagoCore.Resources
{
    public class Disbursement : MercadoPagoBase
    {
        public long? Id { get; set; }
        public decimal? Amount { get; set; }
        public string ExternalReference { get; set; }
        public long? CollectorId { get; set; }
        public decimal? ApplicationFee { get; set; }
        public int? MoneyReleaseDays { get; set; }
        [JsonProperty]
        internal DateTime? MoneyReleaseDate { get; set; }

        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/disbursements/:disbursement_id/disburses")]
        internal static bool UpdateReleaseDate(long advancedPaymentId, long disbursementId, DateTime releaseDate, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "advanced_payment_id", advancedPaymentId.ToString() },
                { "disbursement_id", disbursementId.ToString() }
            };

            Disbursement disbursement = new Disbursement
            {
                MoneyReleaseDate = releaseDate
            };
            return disbursement.ProcessMethodBool<Disbursement>("UpdateReleaseDate", WITHOUT_CACHE, pathParams, requestOptions);
        }
    }
}
