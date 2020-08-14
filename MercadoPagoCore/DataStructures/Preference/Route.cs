namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Route
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string DepartureDateTime { get; set; }
        public string ArrivalDateTime { get; set; }
        public string Company { get; set; }
    }
}
