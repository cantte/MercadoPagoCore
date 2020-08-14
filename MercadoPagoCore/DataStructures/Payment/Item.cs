using System;

namespace MercadoPagoCore.DataStructures.Payment
{
    public struct Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string CategoryId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public CategoryDescriptor CategoryDescriptor { get; set; }
        public bool? Warranty { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
