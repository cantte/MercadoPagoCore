using System;

namespace MercadoPagoCore.Error
{
    public class MercadoPagoException : Exception
    {
        private const string DefaultMessage = "An unexpected error has occurred.";

        public MercadoPagoException(string message, Exception ex) : base(message, ex)
        {
        }

        public MercadoPagoException(Exception ex) : base(DefaultMessage, ex)
        {
        }

        public MercadoPagoException(string message) : base(message)
        {
        }
    }
}
