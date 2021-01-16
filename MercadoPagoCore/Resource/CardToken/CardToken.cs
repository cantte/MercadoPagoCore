using System;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource.CardToken
{
    public class CardToken : IResource
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime? DateDue { get; set; }
        public bool? LuhnValidation { get; set; }
        public bool? LiveMode { get; set; }
        public bool? RequireEsc { get; set; }
        public int? SecurityCodeLength { get; set; }
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
