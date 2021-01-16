using System;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource.PreApproval
{
    public class PreApproval : IResource
    {
        public string Id { get; set; }
        public long? PayerId { get; set; }
        public string PayerEmail { get; set; }
        public string BackUrl { get; set; }
        public long? CollectorId { get; set; }
        public string ApplicationId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string ExternalReference { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public string InitPoint { get; set; }
        public string SandboxInitPoint { get; set; }
        public string PaymentMethodId { get; set; }
        public PreApprovalAutoRecurring AutoRecurring { get; set; }

        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
