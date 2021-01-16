using MercadoPagoCore.Http;

namespace MercadoPagoCore.Resource.User
{
    public class User : IResource
    {
        public long? Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SiteId { get; set; }
        public string CountryId { get; set; }

        public MercadoPagoResponse ApiResponse { get; set; }
    }
}
