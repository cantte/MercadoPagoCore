using MercadoPagoCore.Resource.Common;

namespace MercadoPagoCore.Resource.Customer
{
    /// <summary>
    /// Identification information.
    /// </summary>
    public class CustomerCardCardholderIdentification : Identification
    {
        /// <summary>
        /// Identification subtype.
        /// </summary>
        public string Subtype { get; set; }
    }
}
