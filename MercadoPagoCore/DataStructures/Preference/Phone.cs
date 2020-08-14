using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Phone
    {
        [StringLength(256)]
        public string AreaCode { get; set; }
        [StringLength(256)]
        public string Number { get; set; }
    }
}
