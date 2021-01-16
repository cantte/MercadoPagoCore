using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource.IdentificationType
{
    public class IdentificationType : IResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
