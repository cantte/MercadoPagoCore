using MercadoPagoCore.Core;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class User : MercadoPagoBase
    {
        public long? Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryId { get; set; }


        public static User Find()
        {
            return Find(false, null);
        }

        public static User Find(bool useCache)
        {
            return Find(useCache, null);
        }

        public static User Find(RequestOptions requestOptions)
        {
            return Find(false, requestOptions);
        }

        [GETEndpoint("/users/me")]
        public static User Find(bool useCache, RequestOptions requestOptions)
        {
            return (User)ProcessMethod<User>("Find", null, useCache, requestOptions);
        }
    }
}
