using MercadoPagoCore.Resource.Common;

namespace MercadoPagoCore.Resource.Payment
{
    /// <summary>
    /// Cardholder data.
    /// </summary>
    public class PaymentCardholder
    {
        /// <summary>
        /// Cardholder Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cardholder identification.
        /// </summary>
        public Identification Identification { get; set; }
    }
}
