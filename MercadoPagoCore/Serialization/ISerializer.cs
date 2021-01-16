using System.Threading.Tasks;

namespace MercadoPagoCore.Serialization
{
    public interface ISerializer
    {
        TResponse DeserializeFromJson<TResponse>(string json) where TResponse : new();
        string SerializeToJson(object request);
        Task<string> SerializeToQueryStringAsync(object request);
    }
}
