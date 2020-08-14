namespace MercadoPagoCore.DataStructures.Customer
{
    public struct Address
    {
        public string Id { get; set; }
        public string ZipCode { get; set; }
        public string StreetName { get; set; }
        public int? StreetNumber { get; set; }
    }
}
