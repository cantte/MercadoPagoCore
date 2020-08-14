using System;

namespace MercadoPagoCore.Core.Annotations
{
    public class Idempotent : Attribute
    {
        public Idempotent()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public string GUID { get; set; }
    }
}
