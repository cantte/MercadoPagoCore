namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Shipment
    {
        public bool? LocalPickup { get; set; }
        public ReceiverAddress? ReceiverAddress { get; set; }
        public bool? ExpressShipment { get; set; }
    }
}
