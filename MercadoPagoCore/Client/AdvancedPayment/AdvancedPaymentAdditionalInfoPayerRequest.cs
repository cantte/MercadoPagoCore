using System;
using MercadoPagoCore.Client.Common;

namespace MercadoPagoCore.Client.AdvancedPayment
{
    /// <summary>
    /// Additional info payer information.
    /// </summary>
    public class AdvancedPaymentAdditionalInfoPayerRequest
    {
        /// <summary>
        /// Payer's name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Payer's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Payer's phone.
        /// </summary>
        public PhoneRequest Phone { get; set; }

        /// <summary>
        /// Payer's address.
        /// </summary>
        public AddressRequest Address { get; set; }

        /// <summary>
        /// Date of registration of the payer on your site.
        /// </summary>
        public DateTime? RegistrationDate { get; set; }
    }
}
