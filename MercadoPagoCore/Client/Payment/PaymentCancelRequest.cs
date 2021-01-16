using MercadoPagoCore.Resource.Payment;

namespace MercadoPagoCore.Client.Payment
{
    /// <summary>
    /// Request to cancel a pending payment.
    /// </summary>
    public class PaymentCancelRequest
    {
        /// <summary>
        /// Status cancelled.
        /// </summary>
        public string Status => PaymentStatus.Cancelled;
    }
}
