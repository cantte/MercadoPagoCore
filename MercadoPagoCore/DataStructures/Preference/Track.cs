using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct Track
    {
        [StringLength(256)]
        public string Type { get; set; }
        public JObject Values { get; set; }
    }
}
