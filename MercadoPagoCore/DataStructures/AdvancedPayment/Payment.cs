using System;

namespace MercadoPagoCore.DataStructures.AdvancedPayment
{
    public class Payment
    {
        public long? Id { get; set; }
        public string Status { get; set; }
        public string StatusDetails { get; set; }
        public string PaymentTypeId { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethodOptionId { get; set; }
        public string Token { get; set; }
        public decimal? TransactionAmount { get; set; }
        public int Installments { get; set; }
        public string ProcessingMode { get; set; }
        public string IssuerId { get; set; }
        public string Description { get; set; }
        public bool? Capture { get; set; }
        public string ExternalReference { get; set; }
        public string StatementDescriptor { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public TransactionDetails TransactionDetails { get; set; }
    }
}
