using System.Collections.Generic;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct AdditionalInfo
    {
        public List<Item> Items { get; set; }
        public AdditionalInfoPayer? Payer { get; set; }
        public Shipment? Shipments { get; set; }
        public Barcode? Barcode { get; set; }
        public string IpAddress { get; set; }
    }
}
