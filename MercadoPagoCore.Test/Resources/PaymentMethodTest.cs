using System;
using System.Collections.Generic;
using System.Net;
using MercadoPagoCore.Resources;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture(Ignore = "Skipping")]
    public class PaymentMethodTest
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
        public void PaymentMethod_All_ShouldReturnValues()
        {
            List<PaymentMethod> paymentMethods = PaymentMethod.All();
            Assert.IsTrue(paymentMethods.Count > 1, "Failed: Can't get payment methods");
        }
    }
}
