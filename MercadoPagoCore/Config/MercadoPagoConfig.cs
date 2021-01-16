using System.Configuration;
using System.Reflection;
using MercadoPagoCore.Http;
using MercadoPagoCore.Serialization;

namespace MercadoPagoCore.Config
{
    public static class MercadoPagoConfig
    {
        private const string DefaultProductId = "BC32BHVTRPP001U8NHL0";
        private const int DefaultMaxHttpRetries = 2;
        private const string DefaultBaseUrl = "https://api.mercadopago.com";

        private static string _accessToken;
        private static string _corporationId;
        private static string _integratorId;
        private static string _platformId;
        private static IHttpClient _httpClient;
        private static ISerializer _serializer;
        private static IRetryStrategy _retryStrategy;

        static MercadoPagoConfig()
        {
            System.Version version = new AssemblyName(typeof(MercadoPagoConfig).GetTypeInfo().Assembly.FullName ?? string.Empty).Version;
            Version = version is not null ? version.ToString(3) : "0.0.0";
        }

        public static string Version { get; }

        public static string BaseUrl => DefaultBaseUrl;
        public static string ProductId => DefaultProductId;

        public static string AccessToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_accessToken) &&
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MercadoPagoAccessToken"]))
                {
                    _accessToken = ConfigurationManager.AppSettings["MercadoPagoAccessToken"];
                }

                return _accessToken;
            }
            set => _accessToken = value;
        }

        public static string CorporationId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_corporationId) &&
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MercadoPagoCorporationId"]))
                {
                    _corporationId = ConfigurationManager.AppSettings["MercadoPagoCorporationId"];
                }

                return _corporationId;
            }
            set => _corporationId = value;
        }

        public static string IntegratorId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_integratorId) &&
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MercadoPagoIntegratorId"]))
                {
                    _integratorId = ConfigurationManager.AppSettings["MercadoPagoIntegratorId"];
                }

                return _integratorId;
            }
            set => _integratorId = value;
        }

        public static string PlatformId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_platformId) &&
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MercadoPagoPlatformId"]))
                {
                    _platformId = ConfigurationManager.AppSettings["MercadoPagoPlatformId"];
                }

                return _platformId;
            }
            set => _platformId = value;
        }

        public static IHttpClient HttpClient
        {
            get => _httpClient ??= new DefaultHttpClient();
            set => _httpClient = value;
        }

        public static ISerializer Serializer
        {
            get => _serializer ??= new DefaultSerializer();
            set => _serializer = value;
        }

        public static IRetryStrategy RetryStrategy
        {
            get => _retryStrategy ??= new DefaultRetryStrategy(DefaultMaxHttpRetries);
            set => _retryStrategy = value;
        }
    }
}
