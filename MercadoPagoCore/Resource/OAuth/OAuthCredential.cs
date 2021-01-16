using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource.OAuth
{
    public class OAuthCredential : IResource
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long? ExpiresIn { get; set; }
        public string Scope { get; set; }
        public string RefreshToken { get; set; }
        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
