using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoPagoCore.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMercadoPagoCore(this IServiceCollection services, string accessToken)
        {
            MercadoPagoSDK.AccessToken = accessToken;
            return services;
        }

        public static IServiceCollection AddMeradoPagoCore(this IServiceCollection services, IConfiguration configuration)
        {
            MercadoPagoSDK.AccessToken = configuration["MercadoPagoCore:AccessToken"];
            return services;
        }
    }
}
