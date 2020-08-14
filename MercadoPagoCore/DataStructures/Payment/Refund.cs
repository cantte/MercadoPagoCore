using System;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Refund
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public float Amount { get; set; }
        public JObject Metadata { get; set; }
        public Source? Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UniqueSequenceNumber { get; set; }
    }
}
