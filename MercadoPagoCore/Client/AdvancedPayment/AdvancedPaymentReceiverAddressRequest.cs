using MercadoPagoCore.Client.Common;

namespace MercadoPagoCore.Client.AdvancedPayment
{
    /// <summary>
    /// Receiver address.
    /// </summary>
    public class AdvancedPaymentReceiverAddressRequest : AddressRequest
    {
        /// <summary>
        /// Floor.
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// Apartment.
        /// </summary>
        public string Apartment { get; set; }
    }
}
