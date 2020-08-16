using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MercadoPagoCore.Common;
using MercadoPagoCore.Resources;
using MercadoPagoCore.Test.Helpers;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture]
    public class PaymentTest
    {
        Payment LastPayment;

        [SetUp]
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            LaunchSettingsFixture.LoadLaunchSettings();

            MercadoPagoSDK.CleanConfiguration();
            MercadoPagoSDK.SetBaseUrl("");
            MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
        }

        [Test]
        public void Payment_Create_EmptyShouldFail()
        {
            Payment payment = new Payment();
            payment.Save();

            Assert.IsNotNull(payment.Errors);
            Assert.IsTrue(payment.Errors?.Cause.Length > 0);
        }

        [Test]
        public void Payment_Create_ShouldBeOk()
        {
            Payment payment = PaymentHelper.GetPaymentData(Environment.GetEnvironmentVariable("PUBLIC_KEY"), "pending");
            payment.Save();

            LastPayment = payment;

            Assert.IsTrue(payment.Id.HasValue, "Failed: Payment could not be successfully created");
            Assert.IsTrue(payment.Id.Value > 0, "Failed: Payment has not a valid id");
        }

        [Test]
        public void Payment_FindById_ShouldBeOk()
        {
            Payment payment = Payment.FindById(LastPayment.Id);
            Assert.AreEqual("Pago de prueba", payment.Description);
        }

        [Test]
        public void Payment_Update_ShouldBeOk()
        {
            LastPayment.Status = PaymentStatus.cancelled;
            Assert.True(LastPayment.Update());
        }

        [Test]
        public void Payment_SearchGetListOfPayments()
        {
            List<Payment> payments = Payment.All();

            Assert.IsNotNull(payments);
            Assert.IsTrue(payments.Any());
            Assert.IsTrue(payments.First().Id.HasValue);
        }

        [Test]
        public void Payment_SearchWithFilterGetListOfPayments()
        {
            Dictionary<string, string> filters = new Dictionary<string, string>
            {
                { "external_reference", "INTEGRATION-TEST-PAYMENT" }
            };
            List<Payment> list = Payment.Search(filters);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());
            Assert.IsTrue(list.Last().Id.HasValue);
        }

        [Test]
        public void Payment_Refund()
        {
            Payment OtherPayment = PaymentHelper.GetPaymentData(Environment.GetEnvironmentVariable("PUBLIC_KEY"), "approved");
            OtherPayment.Save();
            OtherPayment.Refund();

            Assert.AreEqual(PaymentStatus.refunded, OtherPayment.Status, "Failed: Payment could not be successfully refunded");
        }
    }
}
