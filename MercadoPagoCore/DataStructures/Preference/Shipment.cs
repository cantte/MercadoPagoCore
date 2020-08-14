using System.Collections.Generic;
using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Shipment
    {
        public ShipmentMode? Mode { get; set; }
        public bool LocalPickup { get; set; }
        public string Dimensions { get; set; }
        public int DefaultShippingMethod { get; set; }
        public List<int> FreeMethods { get; set; }
        public float Cost { get; set; }
        public bool FreeShipping { get; set; }
        public ReceiverAddress? ReceiverAddress { get; set; }
        public bool? ExpressShipment { get; set; }
    }
}
