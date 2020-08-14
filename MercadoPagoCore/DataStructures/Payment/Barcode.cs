using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Barcode
    {
        public EncodingType? Encoding_type { get; set; }
        public string Content { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
