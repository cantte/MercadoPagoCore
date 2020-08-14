using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Address
    {
        [StringLength(256)]
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string FederalUnit { get; set; }
    }
}
