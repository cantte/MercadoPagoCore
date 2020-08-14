using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct PaymentType
    {
        [StringLength(256)]
        public string Id { get; set; }
    }
}
