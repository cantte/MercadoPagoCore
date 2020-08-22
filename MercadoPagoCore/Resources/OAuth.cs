using System.Text;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class OAuth : MercadoPagoBase
    {
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string PublicKey { get; set; }
        public bool? LiveMode { get; set; }
        public long? UserId { get; set; }
        public string TokenType { get; set; }
        public long? ExpiresIn { get; set; }
        public string Scope { get; set; }

        public static string GetAuthorizationURL(string appId, string redirectUri)
        {
            User user = User.Find();
            if (user == null || string.IsNullOrEmpty(user.CountryId))
            {
                return null;
            }

            return new StringBuilder()
                .Append("https://auth.mercadopago.com.")
                .Append(user.CountryId.ToLowerInvariant())
                .Append("/authorization?client_id=")
                .Append(appId)
                .Append("&response_type=code&platform_id=mp&redirect_uri=")
                .Append(redirectUri)
                .ToString();
        }

        public static OAuth GetOAuthCredentials(string authorizationCode, string redirectUri)
        {
            var oAuth = new OAuth
            {
                ClientSecret = MercadoPagoSDK.AccessToken,
                GrantType = "authorization_code",
                Code = authorizationCode,
                RedirectUri = redirectUri
            };
            oAuth.Save();
            return oAuth;
        }

        public static OAuth RefreshOAuthCredentials(string refreshToken)
        {
            var oAuth = new OAuth
            {
                ClientSecret = MercadoPagoSDK.AccessToken,
                GrantType = "refresh_token",
                RefreshToken = refreshToken
            };
            oAuth.Save();
            return oAuth;
        }

        [POSTEndpoint("/oauth/token")]
        public bool Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/oauth/token")]
        public bool Save(RequestOptions requestOptions)
        {
            return ProcessMethodBool<OAuth>("Save", WITHOUT_CACHE, requestOptions);
        }
    }
}
