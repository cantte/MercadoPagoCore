using System;

namespace MercadoPagoCore.Exceptions
{
    [Serializable]
    class ConfigurationException : MercadoPagoException
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}
