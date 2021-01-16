using MercadoPagoCore.Client.AdvancedPayment;
using MercadoPagoCore.Client.CardToken;
using MercadoPagoCore.Client.Customer;
using MercadoPagoCore.Client.IdentificationType;
using MercadoPagoCore.Client.MerchantOrder;
using MercadoPagoCore.Client.OAuth;
using MercadoPagoCore.Client.Payment;
using MercadoPagoCore.Client.PaymentMethod;
using MercadoPagoCore.Client.PreApproval;
using MercadoPagoCore.Client.Preference;
using MercadoPagoCore.Client.User;
using MercadoPagoCore.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoPagoCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMercadoPagoCore(this IServiceCollection services, IConfiguration configuration)
        {
            LoadMercadoPagoConfiguration(configuration);
            ConfigureClients(services);

            return services;
        }

        private static void ConfigureClients(IServiceCollection services)
        {
            services.AddScoped(typeof(AdvancedPaymentClient));
            services.AddScoped(typeof(CardTokenClient));
            services.AddScoped(typeof(CustomerCardClient));
            services.AddScoped(typeof(CustomerClient));
            services.AddScoped(typeof(IdentificationTypeClient));
            services.AddScoped(typeof(MerchantOrderClient));
            services.AddScoped(typeof(OAuthClient));
            services.AddScoped(typeof(PaymentClient));
            services.AddScoped(typeof(PaymentRefundClient));
            services.AddScoped(typeof(PaymentMethodClient));
            services.AddScoped(typeof(PreApprovalClient));
            services.AddScoped(typeof(PreferenceClient));
            services.AddScoped(typeof(UserClient));
        }

        private static void LoadMercadoPagoConfiguration(IConfiguration configuration)
        {
            string accessToken = configuration["MercadoPago:AccessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                MercadoPagoConfig.AccessToken = accessToken;
            }

            string corporationId = configuration["MercadoPago:CorporationId"];
            if (!string.IsNullOrEmpty(corporationId))
            {
                MercadoPagoConfig.CorporationId = corporationId;
            }

            string integratorId = configuration["MercadoPago:IntegratorId"];
            if (!string.IsNullOrEmpty(integratorId))
            {
                MercadoPagoConfig.IntegratorId = integratorId;
            }

            string platformId = configuration["MercadoPago:PlatformId"];
            if (!string.IsNullOrEmpty(platformId))
            {
                MercadoPagoConfig.PlatformId = platformId;
            }
        }
    }
}
