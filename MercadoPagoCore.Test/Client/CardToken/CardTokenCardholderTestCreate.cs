using MercadoPagoCore.Client.Common;

namespace MercadoPagoCore.Tests.Client.CardToken
{
    /// <summary>
    /// Class with cardholder data.
    /// </summary>
    public class CardTokenCardholderTestCreate
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identification.
        /// </summary>
        public IdentificationRequest Identification { get; set; }
    }
}
