namespace MercadoPagoCore.Client.PreApproval
{
    /// <summary>
    /// Data to create a PreApproval.
    /// </summary>
    public class PreApprovalCreateRequest
    {
        /// <summary>
        /// Payer email.
        /// </summary>
        public string PayerEmail { get; set; }

        /// <summary>
        /// Return URL.
        /// </summary>
        public string BackUrl { get; set; }

        /// <summary>
        /// Seller ID.
        /// </summary>
        public long? CollectorId { get; set; }

        /// <summary>
        /// PreApproval title.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// PreApproval reference value.
        /// </summary>
        public string ExternalReference { get; set; }

        /// <summary>
        /// PreApproval status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Recurring data.
        /// </summary>
        public PreApprovalAutoRecurringCreateRequest AutoRecurring { get; set; }
    }
}
