using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct PaymentMethod
    {
        [StringLength(256)]
        public string Id { get; set; }
    }
}
