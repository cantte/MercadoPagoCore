using System;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct AdditionalInfoPayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone? Phone { get; set; }
        public Address? Address { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string AuthenticationType { get; set; }
        public bool? IsPrimeUser { get; set; }
        public bool? IsFirstPurchaseOnline { get; set; }
        public DateTime? LastPurchase { get; set; }
    }
}
