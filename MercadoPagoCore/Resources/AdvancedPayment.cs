using System;
using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AdvPay = MercadoPagoCore.DataStructures.AdvancedPayment;

namespace MercadoPagoCore.Resources
{
    public class AdvancedPayment : MercadoPagoBase
    {
        public long? Id { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public AdvPay.Payer Payer { get; set; }
        public List<AdvPay.Payment> Payments { get; set; }
        public List<Disbursement> Disbursements { get; set; }
        public bool? BinaryMode { get; set; }
        public string ApplicationId { get; set; }
        public bool? Capture { get; set; }
        public string ExternalReference { get; set; }
        public string Description { get; set; }
        public AdvPay.AdditionalInfo AdditionalInfo { get; set; }
        public JObject Metadata { get; set; }
        [JsonProperty]
        internal DateTime? MoneyReleaseDate { get; set; }

        /// <summary>
        /// Save advanced payment data
        /// </summary>
        /// <returns><see langword="true"/> if saved with success, otherwise <see langword="false"/></returns>
        public bool Save()
        {
            return Save(null);
        }

        /// <summary>
        /// Save advanced payment data
        /// </summary>
        /// <param name="requestOptions">Request options</param>
        /// <returns><see langword="true"/> if saved with success, otherwise <see langword="false"/></returns>
        [POSTEndpoint("/v1/advanced_payments")]
        public bool Save(RequestOptions requestOptions)
        {
            return ProcessMethodBool<AdvancedPayment>("Save", WITHOUT_CACHE, requestOptions);
        }

        /// <summary>
        /// Cancels a pending advanced payment
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <returns><see langword="true"/> if cancelled with success, otherwise <see langword="false"/></returns>
        public static bool Cancel(long id)
        {
            return Cancel(id, null);
        }

        /// <summary>
        /// Cancels a pending advanced payment
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns><see langword="true"/> if cancelled with success, otherwise <see langword="false"/></returns>
        [PUTEndpoint("/v1/advanced_payments/:id")]
        public static bool Cancel(long id, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "id", id.ToString() }
            };

            AdvancedPayment advancedPayment = new AdvancedPayment
            {
                Status = "cancelled"
            };
            return advancedPayment.ProcessMethodBool<AdvancedPayment>("Cancel", WITHOUT_CACHE, pathParams, requestOptions);
        }

        /// <summary>
        /// Capture the advanced payment
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <returns><see langword="true"/> if captured with success, otherwise <see langword="false"/></returns>
        [PUTEndpoint("/v1/advanced_payments/:id")]
        public static bool DoCapture(long id)
        {
            return DoCapture(id, null);
        }

        /// <summary>
        /// Capture the advanced payment
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns><see langword="true"/> if captured with success, otherwise <see langword="false"/></returns>
        [PUTEndpoint("/v1/advanced_payments/:id")]
        public static bool DoCapture(long id, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "id", id.ToString() }
            };

            AdvancedPayment advancedPayment = new AdvancedPayment
            {
                Capture = true
            };
            return advancedPayment.ProcessMethodBool<AdvancedPayment>("DoCapture", WITHOUT_CACHE, pathParams, requestOptions);
        }

        /// <summary>
        /// Update money release date for the sellers
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="releaseDate">Release date</param>
        /// <returns><see langword="true"/> if updated with success, otherwise <see langword="false"/></returns>
        public static bool UpdateReleaseDate(long advancedPaymentId, DateTime releaseDate)
        {
            return UpdateReleaseDate(advancedPaymentId, releaseDate, null);
        }

        /// <summary>
        /// Update money release date for the sellers
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="releaseDate">Release date</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns><see langword="true"/> if updated with success, otherwise <see langword="false"/></returns>
        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/disburses")]
        public static bool UpdateReleaseDate(long advancedPaymentId, DateTime releaseDate, RequestOptions requestOptions)
        {
            Dictionary<string, string> pathParams = new Dictionary<string, string>
            {
                { "advanced_payment_id", advancedPaymentId.ToString() }
            };

            AdvancedPayment advancedPayment = new AdvancedPayment
            {
                MoneyReleaseDate = releaseDate
            };
            return advancedPayment.ProcessMethodBool<AdvancedPayment>("UpdateReleaseDate", WITHOUT_CACHE, pathParams, requestOptions);
        }

        /// <summary>
        /// Update money release date for the sellers
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <param name="releaseDate">Release date</param>
        /// <returns><see langword="true"/> if updated with success, otherwise <see langword="false"/></returns>
        public static bool UpdateReleaseDate(long advancedPaymentId, long disbursementId, DateTime releaseDate)
        {
            return UpdateReleaseDate(advancedPaymentId, disbursementId, releaseDate, null);
        }

        /// <summary>
        /// Update money release date for the sellers
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <param name="releaseDate">Release date</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns><see langword="true"/> if updated with success, otherwise <see langword="false"/></returns>
        public static bool UpdateReleaseDate(long advancedPaymentId, long disbursementId, DateTime releaseDate, RequestOptions requestOptions)
        {
            return Disbursement.UpdateReleaseDate(advancedPaymentId, disbursementId, releaseDate, requestOptions);
        }

        /// <summary>
        /// Refund all advanced payment disbursements
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <returns>A list with all disbursement refunds</returns>
        public static List<DisbursementRefund> RefundAll(long advancedPaymentId)
        {
            return RefundAll(advancedPaymentId, null);
        }

        /// <summary>
        /// Refund all advanced payment disbursements
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>A list with all disbursement refunds</returns>
        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/refunds")]
        public static List<DisbursementRefund> RefundAll(long advancedPaymentId, RequestOptions requestOptions)
        {
            return DisbursementRefund.CreateAll(advancedPaymentId, requestOptions);
        }

        /// <summary>
        /// Refund a disbursement
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <returns>Disbursement refund data</returns>
        public static DisbursementRefund Refund(long advancedPaymentId, long disbursementId)
        {
            return Refund(advancedPaymentId, disbursementId, null, null);
        }

        /// <summary>
        /// Refund a disbursement
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>Disbursement refund data</returns>
        public static DisbursementRefund Refund(long advancedPaymentId, long disbursementId, RequestOptions requestOptions)
        {
            return Refund(advancedPaymentId, disbursementId, null, requestOptions);
        }

        /// <summary>
        /// Refund a disbursement
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <param name="amount">Amount to refund</param>
        /// <returns>Disbursement refund data</returns>
        public static DisbursementRefund Refund(long advancedPaymentId, long disbursementId, decimal? amount)
        {
            return Refund(advancedPaymentId, disbursementId, amount, null);
        }

        /// <summary>
        /// Refund a disbursement
        /// </summary>
        /// <param name="advancedPaymentId">Advanced payment ID</param>
        /// <param name="disbursementId">Disbursement ID</param>
        /// <param name="amount">Amount to refund</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>Disbursement refund data</returns>
        [POSTEndpoint("/v1/advanced_payments/:advanced_payment_id/disbursements/:payment_id/refunds")]
        public static DisbursementRefund Refund(long advancedPaymentId, long disbursementId, decimal? amount, RequestOptions requestOptions)
        {
            return DisbursementRefund.Create(advancedPaymentId, disbursementId, amount, requestOptions);
        }

        /// <summary>
        /// Find advanced payment by ID
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <returns>Advanced payment with ID equals to <paramref name="id"/></returns>
        [GETEndpoint("/v1/advanced_payments/:id")]
        public static AdvancedPayment FindById(long? id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Find advanced payment by ID
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>Advanced payment with ID equals to <paramref name="id"/></returns>
        [GETEndpoint("/v1/advanced_payments/:id")]
        public static AdvancedPayment FindById(long? id, RequestOptions requestOptions)
        {
            return FindById(id, WITHOUT_CACHE, requestOptions);
        }

        /// <summary>
        /// Find advanced payment by ID
        /// </summary>
        /// <param name="id">Advanced payment ID</param>
        /// <param name="useCache">Use cache or not</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>Advanced payment with ID equals to <paramref name="id"/></returns>
        [GETEndpoint("/v1/advanced_payments/:id")]
        public static AdvancedPayment FindById(long? id, bool useCache, RequestOptions requestOptions)
        {
            return (AdvancedPayment)ProcessMethod<AdvancedPayment>(typeof(AdvancedPayment), "FindById", id.ToString(), useCache, requestOptions);
        }

        /// <summary>
        /// Get all advanced payments
        /// </summary>
        /// <returns>List of advanced payments</returns>
        public static List<AdvancedPayment> All()
        {
            return All(WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Get all advanced payments
        /// </summary>
        /// <param name="requestOptions">Request options</param>
        /// <returns>List of advanced payments</returns>
        public static List<AdvancedPayment> All(RequestOptions requestOptions)
        {
            return All(WITHOUT_CACHE, requestOptions);
        }

        /// <summary>
        /// Get all advanced payments
        /// </summary>
        /// <param name="useCache">Use cache or not</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>List of advanced payments</returns>
        [GETEndpoint("/v1/advanced_payments/search")]
        public static List<AdvancedPayment> All(bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<AdvancedPayment>(typeof(AdvancedPayment), "Search", useCache, requestOptions);
        }

        /// <summary>
        /// Search advanced payments
        /// </summary>
        /// <param name="filters">Search filters</param>
        /// <returns>List of advanced payments</returns>
        public static List<AdvancedPayment> Search(Dictionary<string, string> filters)
        {
            return Search(filters, WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Search advanced payments
        /// </summary>
        /// <param name="filters">Search filters</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>List of advanced payments</returns>
        public static List<AdvancedPayment> Search(Dictionary<string, string> filters, RequestOptions requestOptions)
        {
            return Search(filters, WITHOUT_CACHE, requestOptions);
        }

        /// <summary>
        /// Search advanced payments
        /// </summary>
        /// <param name="filters">Search filters</param>
        /// <param name="useCache">Use cache or not</param>
        /// <param name="requestOptions">Request options</param>
        /// <returns>List of advanced payments</returns>
        [GETEndpoint("/v1/advanced_payments/search")]
        public static List<AdvancedPayment> Search(Dictionary<string, string> filters, bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<AdvancedPayment>(typeof(AdvancedPayment), "Search", filters, useCache, requestOptions);
        }
    }
}
