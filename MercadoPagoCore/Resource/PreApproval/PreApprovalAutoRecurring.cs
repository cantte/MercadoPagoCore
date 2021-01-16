using System;

namespace MercadoPagoCore.Resource.PreApproval
{
    public class PreApprovalAutoRecurring
    {
        public string CurrencyId { get; set; }
        public decimal? TransactionAmount { get; set; }
        public int? Frequency { get; set; }
        public string FrequencyType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
