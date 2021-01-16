using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource
{
    public interface IResource
    {
        MercadoPagoResponse ApiResponse { get; set; }
    }
}
