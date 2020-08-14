using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.PaymentMethod
{
    public struct SecutiryCode
    {
        public SecurityCodeMode Mode { get; set; }
        public int Length { get; set; }
        public SecurityCodeCardLocation CardLocation { get; set; }
    }
}
