using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Identification
    {
        [StringLength(256)]
        public string Type { get; set; }
        [StringLength(256)]
        public string Number { get; set; }
    }
}
