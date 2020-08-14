using System.Collections.Generic;

namespace MercadoPagoCore.DataStructures.Customer
{
    public struct Shipment
    {
        public bool Success { get; }
        public string Name { get; set; }
        public List<Error> Errors { get; set; }
    }
}
