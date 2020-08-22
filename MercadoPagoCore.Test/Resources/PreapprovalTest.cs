using System;
using System.Net;
using MercadoPagoCore.Common;
using MercadoPagoCore.DataStructures.Preapproval;
using MercadoPagoCore.Resources;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture(Ignore = "Skipping")]
    public class PreapprovalTest
    {
        Preapproval LastPreapproval;

        [SetUp]
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            LaunchSettingsFixture.LoadLaunchSettings();

            MercadoPagoSDK.CleanConfiguration();
            MercadoPagoSDK.ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            MercadoPagoSDK.ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        }

        [Test]
        public void Preapproval_CreateShouldBeOk()
        {
            Preapproval preapproval = new Preapproval
            {
                PayerEmail = "test@test.com",
                BackUrl = "https://localhost.com",
                ExternalReference = "1",
                Reason = "TESTING 1",
                AutoRecurring = new AutoRecurring
                {
                    CurrencyId = CurrencyId.ARS,
                    Frequency = 1,
                    FrequencyType = FrequencyType.months,
                    TransactionAmount = 10
                }
            };

            preapproval.Save();
            LastPreapproval = preapproval;

            Console.WriteLine("INIT POINT: " + preapproval.SandboxInitPoint);

            Assert.IsTrue(preapproval.Id.Length > 0, "Failed: Preapproval could not be successfully created");
            Assert.IsTrue(preapproval.InitPoint.Length > 0, "Failed: Preapproval has not a valid init point");
        }

        [Test]
        public void Preapproval_FindByIDShouldbeOk()
        {
            Preapproval foundedPreapproval = Preapproval.FindById(LastPreapproval.Id);
            Assert.AreEqual(foundedPreapproval.Id, LastPreapproval.Id);
        }

        [Test]
        public void Preapproval_UpdateShouldBeOk()
        {
            LastPreapproval.ExternalReference = "DummyPreapproval for Integration Test";
            LastPreapproval.Update();
            Assert.AreEqual(LastPreapproval.ExternalReference, "DummyPreapproval for Integration Test");
        }
    }
}
