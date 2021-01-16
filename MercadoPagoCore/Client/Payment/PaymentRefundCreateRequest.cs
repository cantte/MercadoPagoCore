﻿namespace MercadoPagoCore.Client.Payment
{
    /// <summary>
    /// Refund creation request data.
    /// </summary>
    public class PaymentRefundCreateRequest
    {
        /// <summary>
        /// Amount to be refunded.
        /// </summary>
        public decimal? Amount { get; set; }
    }
}