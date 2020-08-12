using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FeePayerType
    {
        /// <summary> The seller absorbs the cost </summary>
        collector,
        /// <summary> The buyer absorbs the cost </summary>
        payer
    }
}
