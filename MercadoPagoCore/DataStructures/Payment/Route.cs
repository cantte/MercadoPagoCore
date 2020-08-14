namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Route
    {
        public string Depature { get; set; }
        public string Destination { get; set; }
        public string DepatureDateTime { get; set; }
        public string ArrivalDateTime { get; set; }
        public string Company { get; set; }
    }
}
