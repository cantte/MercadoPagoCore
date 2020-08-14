using System.ComponentModel.DataAnnotations;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.Preapproval;

namespace MercadoPagoCore.Resources
{
    public class Preapproval : MercadoPagoBase
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        [StringLength(256)]
        public string PayerEmail { get; set; }
        [StringLength(500)]
        public string BackUrl { get; set; }
        public string InitPoint { get; set; }
        public string SandboxInitPoint { get; set; }
        public AutoRecurring? AutoRecurring { get; set; }
        [StringLength(256)]
        public string ExternalReference { get; set; }
    }
}
