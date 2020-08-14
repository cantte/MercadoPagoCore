using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct AdditionalInfoAddress
    {
        [StringLength(256)]
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        [StringLength(256)]
        public string ZipCode { get; set; }
    }
}
