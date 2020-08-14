using System.Collections.Generic;

namespace MercadoPagoCore.DataStructures.AdvancedPayment
{
    public class AdditionalInfo
    {
        public string IpAddress { get; set; }
        public List<Item> Items { get; set; }
        public AdditionalInfoPayer Payer { get; set; }
        public Shipments Shipments { get; set; }
    }
}
