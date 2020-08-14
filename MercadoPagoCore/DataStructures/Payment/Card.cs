using System;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Card
    {
        public string Id { get; set; }
        public string LastFourDigits { get; set; }
        public string FirstSixDigits { get; set; }
        public int? ExpirationYear { get; set; }
        public int? ExpirationMonth { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public CardHolder? CardHolder { get; set; }
    }
}
