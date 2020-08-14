using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RefundUserType Type { get; set; }
    }
}
