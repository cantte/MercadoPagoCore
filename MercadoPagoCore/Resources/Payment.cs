using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using MercadoPagoCore.Common;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.Payment;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Resources
{
    public class Payment : MercadoPagoBase
    {
        public long? Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime? MoneyReleaseDate { get; set; }
        public int? CollectorId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType? OperationType { get; set; }
        public Payer Payer { get; set; }
        public bool? BinaryMode { get; set; }
        public bool? LiveMode { get; set; }
        public Order? Order { get; set; }
        public string ExternalReference { get; set; }
        public string Description { get; set; }
        public JObject Metadata { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyId? CurrencyId { get; set; }
        public float? TransactionAmount { get; set; }
        public float? NetAmount { get; set; }
        public float? TransactionAmountRefunded { get; set; }
        public float? CouponAmount { get; set; }
        public int? CampaignId { get; set; }
        public string CouponCode { get; set; }
        public TransactionDetail? TransactionDetails { get; set; }
        public List<FeeDetail> FeeDetails { get; set; } = new List<FeeDetail>();
        public int? DifferentialPricingId { get; set; }
        public float? ApplicationFee { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus? Status { get; set; }
        public string StatusDetail { get; set; }
        public bool? Capture { get; set; }
        public bool? Captured { get; set; }
        public string CallForAuthorizeId { get; set; }
        public string PaymentMethodId { get; set; }
        public string IssuerId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentTypeId? PaymentTypeId { get; set; }
        public string Token { get; set; }
        public DataStructures.Payment.Card? Card { get; set; }
        public string StatementDescriptor { get; set; }
        public int? Installments { get; set; }
        public string NotificationUrl { get; set; }
        public string CallbackUrl { get; set; }
        public List<Refund> Refunds { get; set; } = new List<Refund>();
        public AdditionalInfo? AdditionalInfo { get; set; }
        public string ProcessingMode { get; set; }
        public string MerchantAccountId { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public long? SponsorId { get; set; }
        public List<Taxes> Taxes { get; set; } = new List<Taxes>();
        public string PaymentMethodOptionId { get; set; }
        public MerchantServices MerchantServices { get; set; }
        public string IntegratorId { get; set; }
        public string PlatformId { get; set; }
        public string CorporationId { get; set; }

        public Payment Refund()
        {
            return Refund(null, null);
        }

        public Payment Refund(RequestOptions requestOptions)
        {
            return Refund(null, requestOptions);
        }

        public Payment Refund(decimal amount)
        {
            return Refund(amount, null);
        }

        public Payment Refund(decimal? amount, RequestOptions requestOptions)
        {
            Refund refund = new Refund();
            refund.ManualSetPaymentId((decimal)Id);
            refund.Amount = amount;
            refund.Save(requestOptions);

            if (refund.Id.HasValue)
            {
                Thread.Sleep(100);
                Payment payment = Payment.FindById(Id, WITHOUT_CACHE, requestOptions);
                Status = payment.Status;
                StatusDetail = payment.StatusDetail;
                TransactionAmountRefunded = payment.TransactionAmountRefunded;
                Refunds = payment.Refunds;
            }
            else
            {
                _errors = refund.Errors;
            }

            return this;
        }

        public Payment Load(string id)
        {
            return FindById(long.Parse(id), WITHOUT_CACHE, null);
        }

        public static Payment FindById(long? id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/payments/:id")]
        public static Payment FindById(long? id, bool useCache, RequestOptions requestOptions)
        {
            return (Payment)ProcessMethod<Payment>(typeof(Payment), "FindById", id.ToString(), useCache, requestOptions);
        }

        public bool Save()
        {
            return ProcessMethodBool<Payment>("Save", WITHOUT_CACHE, null);
        }

        [POSTEndpoint("/v1/payments")]
        public bool Save(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Payment>("Save", WITHOUT_CACHE, requestOptions);
        }

        public bool Update()
        {
            return ProcessMethodBool<Payment>("Update", WITHOUT_CACHE, null);
        }

        [PUTEndpoint("/v1/payments/:id")]
        public bool Update(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Payment>("Update", WITHOUT_CACHE, requestOptions);
        }

        public static List<Payment> All()
        {
            return All(WITHOUT_CACHE, null);
        }

        public static List<Payment> Search(Dictionary<string, string> filters)
        {
            return Search(filters, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/payments/search")]
        public static List<Payment> All(bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<Payment>(typeof(Payment), "Search", useCache, requestOptions);
        }

        [GETEndpoint("/v1/payments/search")]
        public static List<Payment> Search(Dictionary<string, string> filters, bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<Payment>(typeof(Payment), "Search", filters, useCache, requestOptions);
        }
    }
}
