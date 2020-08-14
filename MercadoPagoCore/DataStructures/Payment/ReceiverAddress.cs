using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct ReceiverAddress
    {
        [StringLength(256)]
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        [StringLength(256)]
        public string Zip_code { get; set; }
        [StringLength(256)]
        public string Floor { get; set; }
        [StringLength(256)]
        public string Apartment { get; set; }
    }
}
