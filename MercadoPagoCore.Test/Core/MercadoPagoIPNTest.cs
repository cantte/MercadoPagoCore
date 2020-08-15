using System;
using MercadoPagoCore.Core;
using MercadoPagoCore.Exceptions;
using MercadoPagoCore.Resources;
using MercadoPagoCore.Test.Helpers;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Core
{

    public class MercadoPagoIPNTest
    {
        [SetUp]
        public void Init()
        {
            LaunchSettingsFixture.LoadLaunchSettings();
        }

        [Test()]
        public void MPIPN_MustThrowNullException_BothParametersEmpty()
        {
            try
            {
                MercadoPagoIPN.Manage<Payment>(null, null);
            }
            catch (MercadoPagoException exception)
            {
                Assert.AreEqual("Topic and Id can not be null in the IPN request.", exception.Message);
                return;
            }

            Assert.Fail();
        }

        [Test()]
        public void MPIPN_MustThrowException_IdParameterEmpty()
        {
            try
            {
                MercadoPagoIPN.Manage<Payment>(MercadoPagoIPN.Topic.Payment, null);
            }
            catch (MercadoPagoException exception)
            {
                Assert.AreEqual("Topic and Id can not be null in the IPN request.", exception.Message);
                return;
            }

            Assert.Fail();
        }

        [Test()]
        public void MPIPN_MustThrowException_TopicParameterEmpty()
        {
            try
            {
                MercadoPagoIPN.Manage<Payment>(null, "id");
            }
            catch (MercadoPagoException exception)
            {
                Assert.AreEqual("Topic and Id can not be null in the IPN request.", exception.Message);
                return;
            }

            Assert.Fail();
        }

        [Test()]
        public void MPIPN_MustThrowException_ClassNotExtendsFromMPBase()
        {
            try
            {
                MercadoPagoIPN.Manage<Payment>("MercadoPagoCore.DataStructures.Customer.City", "1234567");
            }
            catch (MercadoPagoException exception)
            {
                Assert.AreEqual("City does not extend from MercadoPagoBase", exception.Message);
                return;
            }

            Assert.Fail();
        }

        [Test()]
        public void MPIPN_ShouldBeOk()
        {
            MercadoPagoSDK.CleanConfiguration();
            MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");

            Payment payment = new Payment()
            {
                TransactionAmount = 50000f,
                Token = CardHelper.SingleUseCardToken(Environment.GetEnvironmentVariable("PUBLIC_KEY"), "approved"),
                Description = "Pago de seguro",
                PaymentMethodId = "visa",
                Installments = 1,
                Payer = new DataStructures.Payment.Payer
                {
                    Email = "mlovera@kinexo.com"
                }
            };

            payment.Save();

            var resource = MercadoPagoIPN.Manage<Payment>(MercadoPagoIPN.Topic.Payment, payment.Id.ToString());

            Assert.IsTrue(resource.GetType().IsSubclassOf(typeof(MercadoPagoBase)));
            Assert.AreEqual(payment.Id, ((Payment)resource).Id);
            Assert.AreEqual(payment.Description, ((Payment)resource).Description);
            Assert.AreEqual(payment.PaymentMethodId, ((Payment)resource).PaymentMethodId);
        }

        [Test()]
        public void MPIPN_GetTypeShouldFindTheRightClassType()
        {
            Type type = MercadoPagoIPN.GetType(MercadoPagoIPN.Topic.Payment);
            Assert.IsTrue(typeof(Payment) == type);
        }

        [Test()]
        public void MPIPN_GetTypeShouldReturnNull()
        {
            Type type = MercadoPagoIPN.GetType("MercadoPagoCore.Resources.PatmentMeans");
            Assert.IsNull(type);
        }

        [Test()]
        public void MPIPN_GetTypeShouldReturnNotNullValue()
        {
            Type type = MercadoPagoIPN.GetType(MercadoPagoIPN.Topic.Payment);
            Assert.IsNotNull(type);
        }

        [Test()]
        public void MPIPN_GetTypeShouldReturnATypeObject()
        {
            Type type = MercadoPagoIPN.GetType(MercadoPagoIPN.Topic.Payment);
            if (type != null)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}
