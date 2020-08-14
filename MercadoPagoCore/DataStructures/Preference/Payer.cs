using System;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Payer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Phone? Phone { get; set; }
        public Identification? Identification { get; set; }
        public Address? Address { get; set; }
        public DateTime? DateCreated { get; set; }
        public string AuthenticationType { get; set; }
        public bool? IsPrimeUser { get; set; }
        public bool? IsFirstPurchaseOnline { get; set; }
        public DateTime? LastPurchase { get; set; }
    }
}
