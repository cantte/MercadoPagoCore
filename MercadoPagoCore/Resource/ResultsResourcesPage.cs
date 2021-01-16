using System.Collections.Generic;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource
{
    public class ResultsResourcesPage<TResource> : IResourcesPage<TResource> where TResource : IResource, new()
    {
        public ResultsPaging Paging { get; set; }
        public List<TResource> Results { get; set; }

        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
