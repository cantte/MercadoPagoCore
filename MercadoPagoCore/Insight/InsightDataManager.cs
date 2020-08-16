using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MercadoPagoCore.Insight.DTO;
using Newtonsoft.Json;

namespace MercadoPagoCore.Insight
{
    public sealed class InsightDataManager
    {
        private static readonly string INSIGHT_DEFAULT_BASE_URL = "https://events.mercadopago.com/v2/";
        private static readonly string HEADER_X_INSIGHTS_BUSINESS_FLOW = "X-Insights-Business-Flow";
        private static readonly string HEADER_X_INSIGHTS_METRIC_LAB_SCOPE = "X-Insights-Metric-Lab-Scope";
        private static readonly string HEADER_X_INSIGHTS_DATA_ID = "X-Insights-Data-Id";
        private static readonly string HEADER_X_PRODUCT_ID = "X-Product-Id";
        private static readonly string HEADER_ACCEPT_TYPE = "Accept";
        private static readonly string INSIGHTS_API_ENDPOINT_TRAFFIC_LIGHT = "traffic-light";
        private static readonly string INSIGHTS_API_ENDPOINT_METRIC = "metric";
        private static readonly string HEADER_X_INSIGHTS_EVENT_NAME = "X-Insights-Event-Name";
        private static readonly int DEFAULT_TTL = 600;

        private static InsightDataManager _instance = null;
        private static readonly object simpleLock = new object();

        private long _sendDataDeadline = long.MinValue;
        public TrafficLightResponse TrafficLight { get; private set; }

        InsightDataManager()
        {
            LoadTrafficLight();
        }

        public static InsightDataManager Instance
        {
            get
            {
                lock (simpleLock)
                {
                    if (_instance is null)
                        _instance = new InsightDataManager();
                    return _instance;
                }
            }
        }

        public void SendInsightMetrics(
            HttpWebRequest request,
            HttpWebResponse response,
            SslProtocols? sslProtocol,
            int retries,
            DateTime start,
            DateTime startRequest,
            DateTime endRequest)
        {
            try
            {
                ClientInfo clientInfo = new ClientInfo
                {
                    Name = MercadoPagoSDK.ClientName,
                    Version = MercadoPagoSDK.Version,
                };

                string productId = GetHeaderValue(request, HEADER_X_PRODUCT_ID);
                string businessFlow = GetHeaderValue(request, HEADER_X_INSIGHTS_BUSINESS_FLOW);
                BusinessFlowInfo businessFlowInfo = null;
                if (!string.IsNullOrEmpty(productId) || !string.IsNullOrEmpty(businessFlow))
                {
                    businessFlowInfo = new BusinessFlowInfo
                    {
                        Uid = productId,
                        Name = businessFlow,
                    };
                }

                DTO.EventInfo eventInfo = null;
                string eventName = GetHeaderValue(request, HEADER_X_INSIGHTS_EVENT_NAME);
                if (!string.IsNullOrEmpty(eventName))
                {
                    eventInfo = new DTO.EventInfo
                    {
                        Name = eventName,
                    };
                }

                ProtocolHttp protocolHttp = new ProtocolHttp
                {
                    RequestUrl = request.Address.ToString(),
                    RequestMethod = request.Method,
                    ResponseCode = (int)response.StatusCode,
                    FirstByteTime = startRequest.Subtract(start).Milliseconds,
                    LastByteTime = endRequest.Subtract(startRequest).Milliseconds,
                };

                for (int i = 0; i < request.Headers.Count; i++)
                {
                    string header = request.Headers.GetKey(i);
                    if (header.Equals(HEADER_X_INSIGHTS_DATA_ID, StringComparison.InvariantCultureIgnoreCase)
                        || header.Equals("User-Agent", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    protocolHttp.AddRequestHeader(header, string.Join(";", request.Headers.GetValues(i)));
                }

                for (int i = 0; i < response.Headers.Count; i++)
                {
                    string header = response.Headers.GetKey(i);
                    protocolHttp.AddResponseHeader(header, string.Join(";", response.Headers.GetValues(i)));
                }

                ProtocolInfo protocolInfo = new ProtocolInfo
                {
                    Name = "http",
                    ProtocolHttp = protocolHttp,
                    RetryCount = retries,
                };

                TcpInfo tcpInfo = new TcpInfo
                {
                    SourceAddress = GetHostAddress(),
                    TargetAddress = GetRemoteAddress(request.ServicePoint.Address),
                };

                ConnectionInfo connectionInfo = new ConnectionInfo
                {
                    ProtocolInfo = protocolInfo,
                    TcpInfo = tcpInfo,
                    CertificateInfo = GetCertificateInfo(request, sslProtocol),
                    IsDataComplete = endRequest.Subtract(startRequest).Milliseconds > 0,
                };

                if (!string.IsNullOrEmpty(request.UserAgent))
                {
                    connectionInfo.UserAgent = request.UserAgent;
                }

                DeviceInfo deviceInfo = new DeviceInfo
                {
                    OsName = Environment.OSVersion.VersionString,
                };

                StructuredMetricRequest structuredMetricRequest = new StructuredMetricRequest
                {
                    EventInfo = eventInfo,
                    ClientInfo = clientInfo,
                    BusinessFlowInfo = businessFlowInfo,
                    ConnectionInfo = connectionInfo,
                    DeviceInfo = deviceInfo,
                };

                HttpWebResponse httpResponse = PostData(INSIGHTS_API_ENDPOINT_METRIC, structuredMetricRequest);
                httpResponse.Close();
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        public bool IsInsightMetricsEnabled(string url)
        {
            if (DateTime.Now.Ticks > _sendDataDeadline)
            {
                LoadTrafficLight();
            }

            return TrafficLight.IsSendDataEnabled && TrafficLight.IsEndpointInWhiteList(url);
        }

        private void LoadTrafficLight()
        {
            try
            {
                ClientInfo clientInfo = new ClientInfo
                {
                    Name = MercadoPagoSDK.ClientName,
                    Version = MercadoPagoSDK.Version,
                };

                TrafficLightRequest trafficLightRequest = new TrafficLightRequest
                {
                    ClientInfo = clientInfo,
                };

                HttpWebResponse httpResponse = PostData(INSIGHTS_API_ENDPOINT_TRAFFIC_LIGHT, trafficLightRequest);
                using (StreamReader responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    TrafficLight = JsonConvert.DeserializeObject<TrafficLightResponse>(responseStream.ReadToEnd());
                }

                if (TrafficLight == null)
                {
                    TrafficLight = DefaultTrafficLightResponse();
                }

                _sendDataDeadline = DateTime.Now.Ticks + Math.Abs(TrafficLight.SendTtl * 10000000);
            }
            catch (Exception)
            {
                TrafficLight = DefaultTrafficLightResponse();
            }
        }

        private TrafficLightResponse DefaultTrafficLightResponse()
        {
            return new TrafficLightResponse
            {
                IsSendDataEnabled = false,
                SendTtl = DEFAULT_TTL,
            };
        }

        private HttpWebResponse PostData(string path, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            byte[] payload = Encoding.UTF8.GetBytes(json);

            string url = INSIGHT_DEFAULT_BASE_URL + path;

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.Headers.Add(HEADER_X_INSIGHTS_METRIC_LAB_SCOPE, MercadoPagoSDK.MetricsScope);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Proxy = MercadoPagoSDK.Proxy;
            httpRequest.ContentLength = payload.Length;
            httpRequest.Timeout = MercadoPagoSDK.RequestsTimeout;

            using (Stream requestStream = httpRequest.GetRequestStream())
            {
                requestStream.Write(payload, 0, payload.Length);
            }

            return (HttpWebResponse)httpRequest.GetResponse();
        }

        private string GetHeaderValue(HttpWebRequest httpRequest, string header)
        {
            return httpRequest.Headers[header] ?? "";
        }

        private string GetHostAddress()
        {
            return GetAddress(() => Dns.GetHostName());
        }

        private string GetRemoteAddress(Uri uri)
        {

            return GetAddress(() => uri.Host);
        }

        private string GetAddress(Func<string> getHostname)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(getHostname());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                // Do nothing
            }

            return null;
        }

        private CertificateInfo GetCertificateInfo(HttpWebRequest request, SslProtocols? sslProtocol)
        {
            if (request.ServicePoint.Certificate == null)
            {
                return null;
            }

            try
            {
                X509Certificate2 certificate = new X509Certificate2(request.ServicePoint.Certificate);
                return new CertificateInfo
                {
                    CertificateType = GetSslProtocolType(sslProtocol),
                    CertificateVersion = GetSslProtocolVersion(sslProtocol),
                    CertificateExpiration = certificate.NotAfter.ToString("yyyy-MM-dd'T'HH:mm"),
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetSslProtocolType(SslProtocols? sslProtocol)
        {
            switch (sslProtocol)
            {
                case SslProtocols.Tls:
                case (SslProtocols)768:
                case (SslProtocols)3072:
                case (SslProtocols)12288:
                    return "TLS";
                case SslProtocols.Ssl2:
                case SslProtocols.Ssl3:
                    return "SSL";
                default:
                    return null;
            }
        }

        private string GetSslProtocolVersion(SslProtocols? sslProtocol)
        {
            switch (sslProtocol)
            {
                case SslProtocols.Tls:
                    return "1";
                case (SslProtocols)768:
                    return "1.1";
                case (SslProtocols)3072:
                    return "1.2";
                case (SslProtocols)12288:
                    return "1.3";
                case SslProtocols.Ssl2:
                    return "2";
                case SslProtocols.Ssl3:
                    return "3";
                default:
                    return null;
            }
        }
    }
}
