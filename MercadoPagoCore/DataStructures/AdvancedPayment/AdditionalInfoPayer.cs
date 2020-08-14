using System;

namespace MercadoPagoCore.DataStructures.AdvancedPayment
{
    public class AdditionalInfoPayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
