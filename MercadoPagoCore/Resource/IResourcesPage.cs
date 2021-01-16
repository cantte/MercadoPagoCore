namespace MercadoPagoCore.Resource
{
    public interface IResourcesPage<TResource> : IResource where TResource : IResource, new()
    {
    }
}
