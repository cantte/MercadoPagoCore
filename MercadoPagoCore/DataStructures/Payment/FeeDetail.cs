using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct FeeDetail
    {
        public FeeType? Type { get; set; }
        public FeePayerType? FeePayer { get; set; }
        public float? Amount { get; set; }
    }
}
