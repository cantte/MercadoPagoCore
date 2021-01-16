using System.Collections.Generic;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource
{
    public class ElementsResourcesPage<TResource> : IResourcesPage<TResource> where TResource : IResource, new()
    {
        public int Total { get; set; }
        public int NextOffset { get; set; }
        public List<TResource> Elements { get; set; }
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
