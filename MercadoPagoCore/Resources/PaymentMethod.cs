using System.Collections.Generic;
using MercadoPagoCore.Common;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.PaymentMethod;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class PaymentMethod : MercadoPagoBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PaymentTypeId PaymentTypeId { get; set; }
        public PaymentMethodStatus Status { get; set; }
        public string SecureThumbail { get; set; }
        public string Thumbail { get; set; }
        public PaymentMethodDeferredCapture DeferredCapture { get; set; }
        public List<Settings> Settings { get; set; }
        public List<string> AdditionalInfoNeeded { get; set; }
        public string MinAllowedAmount { get; set; }
        public string MaxAllowedAmount { get; set; }
        public string AccreditationTime { get; set; }
        public List<string> FinancialInstitutions { get; set; }
        public List<string> ProcessingMode { get; set; }

        public static List<PaymentMethod> All()
        {
            return All(WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/payment_methods")]
        public static List<PaymentMethod> All(bool useCache, RequestOptions requestOptions)
        {
            return (List<PaymentMethod>)ProcessMethodBulk<PaymentMethod>(typeof(PaymentMethod), "All", useCache, requestOptions);
        }
    }
}
