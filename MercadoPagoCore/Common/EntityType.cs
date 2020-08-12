using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EntityType
    {
        /// <summary>Payer is individual</summary>
        individual,
        /// <summary>Payer is an association</summary>
        association

    }
}
