using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FrequencyType
    {
        days,
        months
    }
}
