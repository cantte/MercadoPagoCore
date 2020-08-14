using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Tax
    {
        public TaxType Type { get; set; }
        public float? Value { get; set; }
    }
}
