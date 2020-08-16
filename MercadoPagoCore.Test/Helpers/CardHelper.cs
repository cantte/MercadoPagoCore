using System.Collections.Generic;
using System.Linq;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Test.Helpers
{
    public static class CardHelper
    {
        public static string SingleUseCardToken(string publicKey, string status)
        {
            JObject payload = JObject.Parse(CardDummyWithSpecificStatus(status));
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, $"https://api.mercadopago.com/v1/card_tokens?public_key={publicKey}", PayloadType.JSON, payload, null, 0, 1);

            JObject jsonResponse = JObject.Parse(response.StringResponse.ToString());
            List<JToken> tokens = MercadoPagoCoreUtils.FindTokens(jsonResponse, "id");

            return tokens.First().ToString();
        }

        public static string CardDummyWithSpecificStatus(string status)
        {
            Dictionary<string, string> CardsNameForStatus = new Dictionary<string, string>
            {
                {"approved", "APRO"},
                {"pending", "CONT"},
                {"call_for_auth", "CALL"},
                {"not_founds", "FUND"},
                {"expirated", "EXPI"},
                {"form_error", "FORM"},
                {"general_error", "OTHE"}
            };

            return "{ " +
                "\"card_number\": \"5031755734530604\", " +
                "\"security_code\": \"123\", " +
                "\"expiration_month\": \"11\", " +
                "\"expiration_year\": \"2025\", " +
                "\"cardholder\": " +
                    "{ " +
                        "\"name\": \"" + CardsNameForStatus[status] + "\", " +
                        "\"identification\": " +
                        "{ " +
                            "\"type\": \"CC\", " +
                            "\"number\": \"1234567890\" " +
                        "} " +
                    "} " +
                "}";
        }
    }
}
