namespace MercadoPagoCore.Tests.Client
{
    using System;
    using MercadoPagoCore.Client.User;
    using MercadoPagoCore.Config;
    using MercadoPagoCore.Resource.User;

    public class ClientFixture : IDisposable
    {
        public ClientFixture()
        {
            MercadoPagoConfig.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            User = GetUser();
        }

        public User User { get; }

        public void Dispose()
        {
        }

        private static User GetUser()
        {
            var userClient = new UserClient();
            return userClient.Get();
        }
    }
}
