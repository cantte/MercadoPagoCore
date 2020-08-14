namespace MercadoPagoCore.DataStructures.Payment
{
    public struct TransactionDetail
    {
        public string FinancialInstitution { get; set; }
        public string NetReceivedAmount { get; set; }
        public float TotalPaidAmount { get; set; }
        public float InstallmentAmount { get; set; }
        public float OverpaidAmount { get; set; }
        public string ExternalResourceUrl { get; set; }
        public string PaymentMethodReferenceId { get; set; }
    }
}
