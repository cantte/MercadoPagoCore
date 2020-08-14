namespace MercadoPagoCore.DataStructures.AdvancedPayment
{
    public class Payer
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public Identification Identification { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
