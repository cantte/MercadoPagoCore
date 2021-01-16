using System.Collections.Generic;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource
{
    public class ResourcesList<TResource> : List<TResource>, IResource where TResource : IResource, new()
    {
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
