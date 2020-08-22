using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore
{
    public static class MercadoPagoSDK
    {
        private const int DEFAULT_REQUESTS_TIMEOUT = 30000;
        private const int DEFAULT_REQUESTS_RETRIES = 3;
        private const string DEFAULT_BASE_URL = "https://api.mercadopago.com";
        private const string PRODUCT_ID = "BC32BHVTRPP001U8NHL0";
        private const string CLIENT_NAME = "MercadoPago-SDK-DotNet";
        private const string DEFAULT_METRICS_SCOPE = "prod";

        private static string _clientSecret;
        private static string _clientId;
        private static string _accessToken;
        private static string _appId;
        private static int _requestsTimeout = DEFAULT_REQUESTS_TIMEOUT;
        private static int _requestsRetries = DEFAULT_REQUESTS_RETRIES;
        private static IWebProxy _proxy;
        private static string _corporationId;
        private static string _integratorId;
        private static string _platformId;

        static MercadoPagoSDK()
        {
            Version = new AssemblyName(typeof(MercadoPagoSDK).Assembly.FullName).Version.ToString(3);

            TrackingId = $"platform:{Environment.Version.Major}|{Environment.Version},type:MercadoPagoSDK{Version},so;";
        }

        public static string ClientSecret
        {
            get => _clientSecret;
            set
            {
                if (!string.IsNullOrEmpty(_clientSecret))
                    throw new Exceptions.ConfigurationException("ClientSecret setting can not be changed.");
                _clientSecret = value;
            }
        }

        public static string ClientId
        {
            get => _clientId;
            set
            {
                if (!string.IsNullOrEmpty(_clientId))
                    throw new Exceptions.ConfigurationException("ClientId setting can not be changed.");
                _clientId = value;
            }
        }

        /// <summary>
        /// MercadoPago AccessToken
        /// </summary>
        public static string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                if (!string.IsNullOrEmpty(_accessToken))
                    throw new Exceptions.ConfigurationException("AccessToken setting can not be changed.");
                _accessToken = value;
            }
        }

        /// <summary>
        /// Same AccessToken, but from MercadoPagoCredentials
        /// </summary>
        public static string OAuthAccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken))
                    _accessToken = MercadoPagoCredentials.GetAccessToken();
                return _accessToken;
            }
        }

        public static string AppId
        {
            get => _appId;
            set
            {
                if (!string.IsNullOrEmpty(_appId))
                    throw new Exceptions.ConfigurationException("AppId setting can not be changed.");
                _appId = value;
            }
        }

        public static string BaseUrl { get; private set; } = DEFAULT_BASE_URL;

        public static int RequestsTimeout
        {
            get => _requestsTimeout;
            set => _requestsTimeout = value;
        }

        public static int RequestsRetries
        {
            get => _requestsRetries;
            set => _requestsRetries = value;
        }

        public static IWebProxy Proxy { get; set; }
        public static string RefreshToken { get; set; }
        public static string Version { get; private set; }
        public static string ProductId => PRODUCT_ID;
        public static string ClientName => CLIENT_NAME;
        public static string TrackingId { get; private set; }
        public static string MetricsScope { get; set; } = DEFAULT_METRICS_SCOPE;

        /// <summary>
        /// Dictionary based configuration. Valid configuration keys:
        /// ClientSecret, ClientId, AccessToken, AppId
        /// </summary>
        /// <param name="configurationParams"></param>
        public static void SetConfiguration(IDictionary<string, string> configurationParams)
        {
            if (configurationParams is null)
                throw new ArgumentException("Invalid configurationParams parameter");

            configurationParams.TryGetValue("clientSecret", out _clientSecret);
            configurationParams.TryGetValue("clientId", out _clientId);
            configurationParams.TryGetValue("accessToken", out _accessToken);
            configurationParams.TryGetValue("appId", out _appId);

            if (configurationParams.TryGetValue("requestsTimeout", out string requestsTimeout))
            {
                int.TryParse(requestsTimeout, out _requestsTimeout);
            }

            if (configurationParams.TryGetValue("requestsRetries", out string requestsRetries))
            {
                int.TryParse(requestsRetries, out _requestsRetries);
            }

            if (configurationParams.TryGetValue("proxyHostName", out string proxyHostName))
            {
                if (configurationParams.TryGetValue("proxyPort", out string proxyPort))
                {
                    if (int.TryParse(proxyPort, out int _proxyPort))
                    {
                        Proxy = new WebProxy(proxyHostName, _proxyPort);

                        if (configurationParams.TryGetValue("proxyUsername", out string proxyUsername) &&
                            configurationParams.TryGetValue("proxyPassword", out string proxyPassword))
                        {
                            Proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                        }
                    }
                }
            }
        }

        public static void SetConfiguration(Configuration configuration)
        {
            if (configuration is null)
                throw new ArgumentException("Configuration parameter cannot be null");

            IDictionary<string, string> configurationParams = new Dictionary<string, string>
            {
                { "clientSecret", GetConfigValue(configuration, "ClientSecret") },
                { "clientId", GetConfigValue(configuration, "ClientId") },
                { "accessToken", GetConfigValue(configuration, "AccessToken") },
                { "appId", GetConfigValue(configuration, "AppId") },
                { "requestsTimeout", GetConfigValue(configuration, "RequestsTimeout") },
                { "requestsRetries", GetConfigValue(configuration, "RequestsRetries") },
                { "proxyHostName", GetConfigValue(configuration, "ProxyHostName") },
                { "proxyPort", GetConfigValue(configuration, "ProxyPort") },
                { "proxyUsername", GetConfigValue(configuration, "ProxyUsername") },
                { "proxyPassword", GetConfigValue(configuration, "ProxyPassword") }
            };

            SetConfiguration(configurationParams);
        }

        private static string GetConfigValue(Configuration configuration, string key)
        {
            KeyValueConfigurationElement keyValue = configuration.AppSettings.Settings[key];

            if (keyValue != null)
                return keyValue.Value;
            return null;
        }

        /// <summary>
        /// Clean all the configuration variables
        /// (FOR TESTING PURPOSES ONLY)
        /// </summary>
        public static void CleanConfiguration()
        {
            _clientSecret = null;
            _clientId = null;
            _accessToken = null;
            _appId = null;
            BaseUrl = DEFAULT_BASE_URL;
            _requestsTimeout = DEFAULT_REQUESTS_TIMEOUT;
            _requestsRetries = DEFAULT_REQUESTS_RETRIES;
            _proxy = null;
        }

        /// <summary>
        /// Changes base Url
        /// (FOR TESTING PURPOSES ONLY)
        /// </summary>
        public static void SetBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Send a http request with GET method
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static JToken Get(string uri)
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();
            return client.ExecuteGenericRequest(HttpMethod.GET, uri, PayloadType.JSON, null);
        }

        public static JToken Post(string uri, JObject payload)
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();
            return client.ExecuteGenericRequest(HttpMethod.POST, uri, PayloadType.JSON, payload);
        }

        public static JToken Put(string uri, JObject payload)
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();
            return client.ExecuteGenericRequest(HttpMethod.PUT, uri, PayloadType.JSON, payload);
        }

        public static string CorporationId
        {
            get { return _corporationId; }
            set
            {
                if (!string.IsNullOrEmpty(_corporationId))
                {
                    throw new Exceptions.ConfigurationException("CorporationId setting can not be changed");
                }
                _corporationId = value;
            }
        }

        public static string IntegratorId
        {
            get { return _integratorId; }
            set
            {
                if (!string.IsNullOrEmpty(_integratorId))
                {
                    throw new Exceptions.ConfigurationException("integratorId setting can not be changed");
                }
                _integratorId = value;
            }
        }

        public static string PlatformId
        {
            get { return _platformId; }
            set
            {
                if (!string.IsNullOrEmpty(_platformId))
                {
                    throw new Exceptions.ConfigurationException("platformId setting can not be changed");
                }
                _platformId = value;
            }
        }
    }
}
