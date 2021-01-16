using MercadoPagoCore.Resource.Common;

namespace MercadoPagoCore.Resource.AdvancedPayment
{
    /// <summary>
    /// Receiver address.
    /// </summary>
    public class AdvancedPaymentReceiverAddress : Address
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
