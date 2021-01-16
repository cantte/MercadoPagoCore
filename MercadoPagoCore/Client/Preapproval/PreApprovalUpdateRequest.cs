namespace MercadoPagoCore.Client.PreApproval
{
    /// <summary>
    /// Data to update a PreApproval.
    /// </summary>
    public class PreApprovalUpdateRequest
    {
        /// <summary>
        /// Return URL.
        /// </summary>
        public string BackUrl { get; set; }

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
        public PreApprovalAutoRecurringUpdateRequest AutoRecurring { get; set; }
    }
}
