namespace MercadoPagoCore.DataStructures.PaymentMethod
{
    public struct Settings
    {
        public Bin Bin { get; set; }
        public CardNumber CardNumber { get; set; }
        public SecutiryCode SecutiryCode { get; set; }
    }
}
