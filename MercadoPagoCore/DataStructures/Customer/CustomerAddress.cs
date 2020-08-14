using System;

namespace MercadoPagoCore.DataStructures.Customer
{
    public struct CustomerAddress
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public City? City { get; set; }
        public State? State { get; set; }
        public Country? Country { get; set; }
        public Neighborhood? Neighborhood { get; set; }
        public Municipality? Municipality { get; set; }
        public string Comments { get; set; }
        public DateTime? DateCreated { get; set; }
        public Verification Verifications { get; set; }
        public bool LiveMode { get; set; }
    }
}
