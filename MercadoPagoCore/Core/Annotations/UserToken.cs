using System;

namespace MercadoPagoCore.Core.Annotations
{
    /// <summary>
    /// Attribute to define a custom user token for a certain resource
    /// </summary>
    public class UserToken : Attribute
    {
        public UserToken(string token)
        {
            UserAsignedToken = token;
        }

        public string UserAsignedToken { get; }
    }
}
