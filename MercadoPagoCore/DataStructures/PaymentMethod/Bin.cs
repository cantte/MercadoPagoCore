namespace MercadoPagoCore.DataStructures.PaymentMethod
{
    public struct Bin
    {
        public string Pattern { get; set; }
        public string ExclusionPattern { get; set; }
        public string InstallmentsPattern { get; set; }
    }
}
