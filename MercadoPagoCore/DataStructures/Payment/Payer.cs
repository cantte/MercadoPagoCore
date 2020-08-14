using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Payer
    {
        public EntityType? Entity_type { get; set; }
        public PayerType? Type { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public Identification? Identification { get; set; }
        public Phone? Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address? Address { get; set; }
    }
}
