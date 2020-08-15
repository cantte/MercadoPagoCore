using System;
using System.Text.Json.Serialization;
using MercadoPagoCore.Common;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string CategoryId { get; set; }
        public int? Quantity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyId CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public CategoryDescriptor CategoryDescriptor { get; set; }
        public bool? Warranty { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
