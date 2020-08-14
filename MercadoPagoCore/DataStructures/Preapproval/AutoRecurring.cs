using System;
using System.ComponentModel.DataAnnotations;
using MercadoPagoCore.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.DataStructures.Preapproval
{
    public struct AutoRecurring
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FrequencyType FrequencyType { get; set; }
        public int Frecuency { get; set; }
        public float TransactionAmount { get; set; }
        [StringLength(3)]
        public CurrencyId CurrencyId { get; set; }
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
    }
}
