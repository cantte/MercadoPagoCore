using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Phone
    {
        [StringLength(256)]
        public string AreaCode { get; set; }
        [StringLength(256)]
        public string Number { get; set; }
        public string Extension { get; set; }
    }
}
