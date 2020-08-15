using System;
using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MercadoPagoCore.Test
{
    [TestFixture(Ignore = "Skipping")]
    public class MercadoPagoSDKTest
    {
        [SetUp]
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            LaunchSettingsFixture.LoadLaunchSettings();

            MercadoPagoSDK.CleanConfiguration();
            MercadoPagoSDK.SetBaseUrl("https://api.mercadopago.com");
            MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
        }

        [Test]
        public void GenericGetMethod()
        {
            JToken results = MercadoPagoSDK.Get("/v1/sites");
            Assert.IsNotNull(results);
        }

        [Test]
        public void GenericPostMethod()
        {

            JObject preference = new JObject(
                new JProperty("items", new JArray(
                    new JObject(
                        new JProperty("title", "Dummy Item"),
                        new JProperty("description", "Multicolor Item"),
                        new JProperty("quantity", 3),
                        new JProperty("unit_price", 10.0)))),

                new JProperty("payer", new JArray(
                    new JObject(
                        new JProperty("email", "demo@gmail.com")
                    )
                ))
            );

            JToken results = MercadoPagoSDK.Post("/checkout/preferences", preference);

            Assert.IsNotNull(results);
        }
    }
}
