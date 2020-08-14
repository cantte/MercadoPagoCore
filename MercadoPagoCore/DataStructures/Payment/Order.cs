using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Order
    {
        public OrderType? Type { get; set; }
        public long? Id1 { get; set; }
    }
}
