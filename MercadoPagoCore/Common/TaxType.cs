using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.Common
{
    [JsonConverter(typeof(StringEnumConverter))]

    public enum TaxType
    {
        /// <summary>
        /// IVA tax
        /// </summary>
        IVA,
        /// /// <summary>
        /// INC tax
        /// </summary>
        INC
    }
}
