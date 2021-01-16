using System;
using MercadoPagoCore.Http;
using MercadoPagoCore.Resource.Common;

namespace MercadoPagoCore.Resource.Payment
{
    /// <summary>
    /// Payment refund data.
    /// </summary>
    public class PaymentRefund : IResource
    {
        /// <summary>
        /// Refund id.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// ID of the refunded payment.
        /// </summary>
        public long? PaymentId { get; set; }

        /// <summary>
        /// Amount refunded.
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Refund status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Refund mode.
        /// </summary>
        public string RefundMode { get; set; }

        /// <summary>
        /// Date of creation.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Unique sequence number.
        /// </summary>
        public string UniqueSequenceNumber { get; set; }

        /// <summary>
        /// Source of the refund.
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Response from API.
        /// </summary>
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
