using System;
using System.Collections.Generic;
using System.Net;
using MercadoPagoCore.Common;
using MercadoPagoCore.DataStructures.Preference;
using MercadoPagoCore.Resources;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture]
    public class PreferenceTest
    {
        Preference LastPreference;

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
        public void Preference_CreateShouldBeOk()
        {
            Shipment shipments = new Shipment
            {
                ReceiverAddress = new ReceiverAddress
                {
                    ZipCode = "28834",
                    StreetName = "Torrente Antonia",
                    StreetNumber = 1219,
                    Floor = "8",
                    Apartment = "C"
                }
            };

            List<PaymentType> excludedPaymentTypes = new List<PaymentType>
            {
                new PaymentType
                {
                    Id = "ticket"
                }
            };

            Preference preference = new Preference
            {
                ExternalReference = "01-02-00000003",
                Expires = true,
                ExpirationDateFrom = DateTime.Now,
                ExpirationDateTo = DateTime.Now.AddDays(1),
                PaymentMethods = new PaymentMethods
                {
                    ExcludedPaymentTypes = excludedPaymentTypes
                }
            };

            preference.Items.Add(
                new Item
                {
                    Title = "Dummy Item",
                    Description = "Multicolor Item",
                    Quantity = 1,
                    UnitPrice = 10.0M
                }
            );

            preference.Shipments = shipments;

            preference.ProcessingModes.Add(ProcessingMode.aggregator);

            preference.Save();
            LastPreference = preference;

            Console.WriteLine("INIT POINT: " + preference.InitPoint);

            Assert.IsTrue(preference.Id.Length > 0, "Failed: Payment could not be successfully created");
            Assert.IsTrue(preference.InitPoint.Length > 0, "Failed: Preference has not a valid init point");
        }

        [Test]
        public void Preference_UpdateShouldBeOk()
        {
            LastPreference.ExternalReference = "DummyPreference for Integration Test";
            LastPreference.Update();
            Assert.AreEqual(LastPreference.ExternalReference, "DummyPreference for Integration Test");
        }

        [Test]
        public void Preference_CreateWithCurrencyIdShouldBeOk()
        {
            Item item = new Item
            {
                Title = "Dummy Item",
                Description = "Multicolor Item",
                Quantity = 1,
                UnitPrice = 10.0M,
                CurrencyId = CurrencyId.ARS
            };

            Preference preference = new Preference();
            preference.Items.Add(item);

            preference.Save();
            LastPreference = preference;

            Assert.IsTrue(preference.Id.Length > 0, "Failed: Preference could not be successfully created");
            Assert.IsTrue(preference.InitPoint.Length > 0, "Failed: Preference has not a valid init point");
        }
    }
}
