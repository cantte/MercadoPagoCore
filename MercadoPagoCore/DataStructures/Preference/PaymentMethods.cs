using System.Collections.Generic;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct PaymentMethods
    {
        private int? _installments;
        public List<PaymentMethod> ExcludedPaymentMethods { get; set; }
        public List<PaymentType> ExcludedPaymentTypes { get; set; }
        public string DefaultPaymentMethodId { get; set; }
        public int? Installments
        {
            get
            {
                return _installments ?? 1;
            }
            set
            {
                _installments = value;
            }
        }
        public int? DefaultInstallments { get; set; }
    }
}
