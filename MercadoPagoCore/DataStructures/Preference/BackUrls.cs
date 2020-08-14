using System.ComponentModel.DataAnnotations;

namespace MercadoPagoCore.DataStructures.Preference
{
    public struct BackUrls
    {
        [StringLength(600)]
        public string Success { get; set; }
        [StringLength(600)]
        public string Pending { get; set; }
        [StringLength(600)]
        public string Failure { get; set; }
    }
}
