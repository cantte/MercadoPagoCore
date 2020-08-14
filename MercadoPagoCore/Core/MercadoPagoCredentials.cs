using System.Collections.Generic;
using System.Linq;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Exceptions;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Core
{
    public static class MercadoPagoCredentials
    {
        public static string GetAccessToken()
        {
            if (string.IsNullOrEmpty(MercadoPagoSDK.ClientId) || string.IsNullOrEmpty(MercadoPagoSDK.ClientSecret))
            {
                throw new MercadoPagoException("\"client_id\" and \"client_secret\" can not be \"null\" when getting the \"access_token\"");
            }

            JObject jsonPayload = new JObject
            {
                { "grant_type", "client_credentials" },
                { "client_id", MercadoPagoSDK.ClientId },
                { "client_secret", MercadoPagoSDK.ClientSecret }
            };
            string access_token;
            MercadoPagoAPIResponse response = new MercadoPagoRestClient().ExecuteRequest(
                    HttpMethod.POST,
                    MercadoPagoSDK.BaseUrl + "/oauth/token",
                    PayloadType.X_WWW_FORM_URLENCODED,
                    jsonPayload,
                    null,
                    0,
                    0);

            JObject jsonResponse = JObject.Parse(response.StringResponse.ToString());

            if (response.StatusCode == 200)
            {
                List<JToken> accessTokenElem = MercadoPagoCoreUtils.FindTokens(jsonResponse, "access_token");
                List<JToken> refreshTokenElem = MercadoPagoCoreUtils.FindTokens(jsonResponse, "refresh_token");

                if (accessTokenElem != null && accessTokenElem.Count == 1)
                    access_token = accessTokenElem.First().ToString();
                else
                    throw new MercadoPagoException("Can not retrieve the \"access_token\"");

                // Making refresh token an optional param
                if (refreshTokenElem != null && refreshTokenElem.Count == 1)
                {
                    string refresh_token = refreshTokenElem.First().ToString();
                    MercadoPagoSDK.RefreshToken = refresh_token;
                }
            }
            else
            {
                throw new MercadoPagoException("Can not retrieve the \"access_token\"");
            }


            return access_token;
        }
    }
}
