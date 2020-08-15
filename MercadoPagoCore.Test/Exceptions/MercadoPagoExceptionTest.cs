using MercadoPagoCore.Exceptions;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Exceptions
{
    [TestFixture()]
    public class MercadoPagoExceptionTest
    {
        [Test()]
        public void MercadoPagoExceptionTestToStringOverride()
        {
            MercadoPagoException exception = new MercadoPagoException("Some Exception message");
            Assert.AreEqual("MercadoPagoCore.Exceptions.MercadoPagoException: Some Exception message", exception.ToString().Trim());

            MercadoPagoException exception2 = new MercadoPagoException("Some Exception message 2", new MercadoPagoException("InnerExceptionMessage 2"));
            Assert.AreEqual("MercadoPagoCore.Exceptions.MercadoPagoException: InnerExceptionMessage 2", exception2.InnerException.ToString().Trim());

            MercadoPagoException exception3 = new MercadoPagoException("Some Exception message 3", "requestId", 666);
            Assert.AreEqual(string.Format("MercadoPagoCore.Exceptions.MercadoPagoException: Some Exception message 3; request-id: {0}; status_code: {1}", "requestId", 666), exception3.ToString().Trim());

            MercadoPagoException exception4 = new MercadoPagoException("Some Exception message 4", "requestId", 666, new MercadoPagoException("InnerExceptionMessage 4"));
            Assert.AreEqual("MercadoPagoCore.Exceptions.MercadoPagoException: InnerExceptionMessage 4", exception4.InnerException.ToString().Trim());
        }
    }
}
