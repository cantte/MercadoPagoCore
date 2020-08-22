using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class DiscountCampaign : MercadoPagoBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float? PercentOff { get; set; }
        public float? AmountOff { get; set; }
        public float? CouponAmount { get; set; }
        public string CurrencyId { get; set; }

        public static DiscountCampaign Find(float transactionAmount, string payerEmail)
        {
            return Find(transactionAmount, payerEmail, null, WITHOUT_CACHE, null);
        }

        public static DiscountCampaign Find(float transactionAmount, string payerEmail, bool useCache, RequestOptions requestOptions)
        {
            return Find(transactionAmount, payerEmail, null, useCache, requestOptions);
        }

        public static DiscountCampaign Find(float transactionAmount, string payerEmail, string couponCode)
        {
            return Find(transactionAmount, payerEmail, couponCode, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/discount_campaigns")]
        public static DiscountCampaign Find(float transactionAmount, string payerEmail, string couponCode, bool useCache, RequestOptions requestOptions)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "transaction_amount", transactionAmount.ToString() },
                { "payer_email", payerEmail },
                { "coupon_code", couponCode }
            };

            return ProcessMethod<DiscountCampaign>(typeof(DiscountCampaign), null, "Find", queryParams, useCache, requestOptions);
        }

    }
}
