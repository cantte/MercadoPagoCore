using System.Collections.Generic;

namespace MercadoPagoCore.Resource.Preference
{
    /// <summary>
    /// Payment methods information from <see cref="Preference"/>.
    /// </summary>
    public class PreferencePaymentMethods
    {
        /// <summary>
        /// Payment methods not allowed in payment process (except account_money).
        /// </summary>
        public List<PreferencePaymentMethod> ExcludedPaymentMethods { get; set; }

        /// <summary>
        /// Payment types not allowed in payment process.
        /// </summary>
        public List<PreferencePaymentType> ExcludedPaymentTypes { get; set; }

        /// <summary>
        /// Payment method to be preferred on the payments methods list.
        /// </summary>
        public string DefaultPaymentMethodId { get; set; }

        /// <summary>
        /// Maximum number of credit card installments to be accepted.
        /// </summary>
        public int? Installments { get; set; }

        /// <summary>
        /// Prefered number of credit card installments.
        /// </summary>
        public int? DefaultInstallments { get; set; }
    }
}
