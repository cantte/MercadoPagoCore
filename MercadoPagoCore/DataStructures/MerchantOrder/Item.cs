using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.MerchantOrder
{
    public struct Item
    {
        public string ID { get; set; }
        public string CategoryId { get; set; }
        [RegularExpression(@"^.{3,3}$")]
        public string CurrencyId { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string Title { get; set; }
    }
}
