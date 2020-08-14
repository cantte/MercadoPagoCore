using System;

namespace MercadoPagoCore.Exceptions
{
    [Serializable]
    public class ConfigurationException : MercadoPagoException
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}
