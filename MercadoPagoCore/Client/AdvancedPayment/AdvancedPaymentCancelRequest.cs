using MercadoPagoCore.Resource.AdvancedPayment;

namespace MercadoPagoCore.Client.AdvancedPayment
{
    /// <summary>
    /// Request to cancel a pending payment.
    /// </summary>
    public class AdvancedPaymentCancelRequest
    {
        /// <summary>
        /// Status cancelled.
        /// </summary>
        public string Status => AdvancedPaymentStatus.Cancelled;
    }
}
