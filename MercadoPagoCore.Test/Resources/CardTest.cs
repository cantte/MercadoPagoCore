using System;
using System.Net;
using MercadoPagoCore.Resources;
using MercadoPagoCore.Test.Helpers;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture(Ignore = "Skipping")]
    public class CardTest
    {
        Customer LastCustomer;
        Card LastCard;

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
        public void Card_CreateShouldBeOk()
        {
            Customer customer = new Customer
            {
                Email = "temp.customer@gmail.com"
            };
            customer.Save();

            Card card = new Card
            {
                CustomerId = customer.Id,
                Token = CardHelper.SingleUseCardToken(Environment.GetEnvironmentVariable("PUBLIC_KEY"), "pending")
            };
            card.Save();

            LastCustomer = customer;
            LastCard = card;

            Assert.IsNotEmpty(card.Id);
        }

        [Test]
        public void Card_FindById_ShouldBeOk()
        {
            Card card = Card.FindById(LastCustomer.Id, LastCard.Id);
            Assert.IsNotNull(card);
            Assert.IsNotEmpty(card.Id);
        }

        [Test]
        public void Card_UpdateShouldBeOk()
        {
            string lastToken = LastCard.Token;
            LastCard.Token = CardHelper.SingleUseCardToken(Environment.GetEnvironmentVariable("PUBLIC_KEY"), "not_founds");
            LastCard.Update();

            Assert.AreNotEqual(lastToken, LastCard.Token);
        }

        [Test]
        public void RemoveCard()
        {
            LastCard.Delete();
            LastCustomer.Delete();
        }
    }
}
