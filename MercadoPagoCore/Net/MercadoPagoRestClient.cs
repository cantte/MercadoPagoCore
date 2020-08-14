using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Exceptions;
using MercadoPagoCore.Insight;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Net
{
    public class MercadoPagoRestClient
    {
        private readonly IWebProxy _proxy;

        public MercadoPagoRestClient()
        {
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType.Tls11) | (SecurityProtocolType.Tls12);
        }

        public MercadoPagoRestClient(string proxyHostName, int proxyPort) : this()
        {
            _proxy = new WebProxy(proxyHostName, proxyPort);
            ProxyHostName = ProxyHostName;
            ProxyPort = proxyPort;
        }

        public string ProxyHostName { get; set; }
        public int ProxyPort { get; set; }

        public JToken ExecuteGenericRequest(HttpMethod httpMethod, string path, PayloadType payloadType, JObject payload)
        {
            if (MercadoPagoSDK.AccessToken != null)
            {
                path = MercadoPagoSDK.BaseUrl + path + "?access_token=" + MercadoPagoSDK.AccessToken;
            }

            RequestBase request = CreateRequest(httpMethod, path, payloadType, payload, null, 0, 0);

            if (new HttpMethod[] { HttpMethod.POST, HttpMethod.PUT }.Contains(httpMethod))
            {
                Stream requestStream = request.Request.GetRequestStream();
                requestStream.Write(request.RequestPayload, 0, request.RequestPayload.Length);
                requestStream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.Request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                    string StringResponse = reader.ReadToEnd();
                    return JToken.Parse(StringResponse);
                }

            }
            catch (WebException ex)
            {
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                Stream dataStream = errorResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                string StringResponse = reader.ReadToEnd();
                return JToken.Parse(StringResponse);
            }
        }

        public MercadoPagoAPIResponse ExecuteRequest(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            WebHeaderCollection colHeaders,
            int requestTimeout,
            int retries)
        {
            var requestOptions = CreateRequestOptions(colHeaders, requestTimeout, retries);
            return ExecuteRequest(httpMethod, path, payloadType, payload, requestOptions);
        }

        public MercadoPagoAPIResponse ExecuteRequest(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload)
        {
            return ExecuteRequest(httpMethod, path, payloadType, payload, null);
        }

        public MercadoPagoAPIResponse ExecuteRequest(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            RequestOptions requestOptions)
        {
            DateTime start = DateTime.Now;

            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            RequestBase request = CreateRequest(httpMethod, path, payloadType, payload, requestOptions);

            if (new HttpMethod[] { HttpMethod.POST, HttpMethod.PUT }.Contains(httpMethod))
            {
                using (Stream requestStream = request.Request.GetRequestStream())
                {
                    requestStream.Write(request.RequestPayload, 0, request.RequestPayload.Length);
                }
            }

            try
            {
                Int32 retries;
                DateTime startRequest = DateTime.Now;
                var response = ExecuteRequest(request.Request, requestOptions.Retries, out retries);
                DateTime endRequest = DateTime.Now;

                // Send metrics
                SendMetrics(request.Request, response, retries, start, startRequest, endRequest);

                return new MercadoPagoAPIResponse(httpMethod, request.Request, payload, response);
            }
            catch (Exception ex)
            {
                throw new MercadoPagoRestException(ex.Message);
            }
        }

        public RequestBase CreateRequest(HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            WebHeaderCollection colHeaders,
            int connectionTimeout,
            int retries)
        {
            var requestOptions = CreateRequestOptions(colHeaders, connectionTimeout, retries);
            return CreateRequest(httpMethod, path, payloadType, payload, requestOptions);
        }

        public RequestBase CreateRequest(HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            RequestOptions requestOptions)
        {

            if (string.IsNullOrEmpty(path))
                throw new MercadoPagoRestException("Uri can not be an empty string.");

            if (httpMethod.Equals(HttpMethod.GET))
            {
                if (payload != null)
                {
                    throw new MercadoPagoRestException("Payload not supported for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.POST))
            {
                //if (payload == null)
                //{
                //    throw new MercadoPagoRestException("Must include payload for this method.");
                //}
            }
            else if (httpMethod.Equals(HttpMethod.PUT))
            {
                if (payload == null)
                {
                    throw new MercadoPagoRestException("Must include payload for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.DELETE))
            {
                if (payload != null)
                {
                    throw new MercadoPagoRestException("Payload not supported for this method.");
                }
            }

            RequestBase request = new RequestBase
            {
                Request = (HttpWebRequest)WebRequest.Create(path)
            };
            request.Request.Method = httpMethod.ToString();

            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            if (requestOptions.Timeout > 0)
            {
                request.Request.Timeout = requestOptions.Timeout;
            }

            request.Request.Headers.Add("x-product-id", MercadoPagoSDK.ProductId);
            request.Request.Headers.Add("x-tracking-id", MercadoPagoSDK.TrackingId);

            if (requestOptions.CustomHeaders != null)
            {
                foreach (var header in requestOptions.CustomHeaders)
                {
                    if (request.Request.Headers[header.Key] == null)
                    {
                        request.Request.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            if (requestOptions.TrackHeaders != null)
            {
                foreach (var trackHeader in requestOptions.TrackHeaders)
                {
                    if (request.Request.Headers[trackHeader.Key] == null && trackHeader.Value != null)
                    {
                        request.Request.Headers[trackHeader.Key] = trackHeader.Value;
                    }
                }
            }

            if (payload != null) // POST & PUT
            {
                byte[] data = null;
                if (payloadType != PayloadType.JSON)
                {
                    var parametersDict = payload.ToObject<Dictionary<string, string>>();
                    StringBuilder parametersString = new StringBuilder();
                    parametersString.Append(string.Format("{0}={1}", parametersDict.First().Key, parametersDict.First().Value));
                    parametersDict.Remove(parametersDict.First().Key);
                    foreach (var value in parametersDict)
                    {
                        parametersString.Append(string.Format("&{0}={1}", value.Key, value.Value.ToString()));
                    }

                    data = Encoding.ASCII.GetBytes(parametersString.ToString());
                }
                else
                {
                    data = Encoding.ASCII.GetBytes(payload.ToString());
                }

                request.Request.UserAgent = "MercadoPago DotNet MercadoPagoSDK/" + MercadoPagoSDK.Version;
                request.Request.ContentLength = data.Length;
                request.Request.ContentType = payloadType == PayloadType.JSON ? "application/json" : "application/x-www-form-urlencoded";
                request.RequestPayload = data;
            }

            IWebProxy proxy = requestOptions.Proxy != null ? requestOptions.Proxy : (_proxy != null ? _proxy : MercadoPagoSDK.Proxy);
            request.Request.Proxy = proxy;

            return request;
        }

        private RequestOptions CreateRequestOptions(WebHeaderCollection colHeaders, int connectionTimeout, int retries)
        {
            IDictionary<String, String> headers = new Dictionary<String, String>();
            if (colHeaders != null)
            {
                foreach (var header in colHeaders)
                {
                    headers.Add(header.ToString(), colHeaders[header.ToString()]);
                }
            }

            return new RequestOptions
            {
                Timeout = connectionTimeout,
                Retries = retries,
                CustomHeaders = headers
            };
        }

        private HttpWebResponse ExecuteRequest(HttpWebRequest request, Int32 maxRetries, out Int32 retries)
        {
            retries = 0;
            while (true)
            {
                try
                {
                    return (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        return ex.Response as HttpWebResponse;
                    }

                    if (++retries > maxRetries)
                        throw;
                }
            }
        }

        private void SendMetrics(HttpWebRequest request, HttpWebResponse response, int retries, DateTime start, DateTime startRequest, DateTime endRequest)
        {
            try
            {
                var sslProtocol = GetSslProtocol(response.GetResponseStream());
                var metricsSender = new MetricsSender(request, response, sslProtocol, retries, start, startRequest, endRequest);
                metricsSender.Send();
            }
            catch
            {
                // Do nothing
            }
        }

        private SslProtocols? GetSslProtocol(Stream stream)
        {
            if (stream == null)
                return null;

            try
            {
                if (typeof(SslStream).IsAssignableFrom(stream.GetType()))
                {
                    var ssl = stream as SslStream;
                    return ssl.SslProtocol;
                }

                var flags = BindingFlags.NonPublic | BindingFlags.Instance;

                if (stream.GetType().FullName == "System.Net.ConnectStream")
                {
                    var connection = stream.GetType().GetProperty("Connection", flags).GetValue(stream, null);
                    var netStream = connection.GetType().GetProperty("NetworkStream", flags).GetValue(connection, null) as Stream;
                    return GetSslProtocol(netStream);
                }

                if (stream.GetType().FullName == "System.Net.WebRequestStream" || stream.GetType().FullName == "System.Net.WebResponseStream")
                {
                    var connection = stream.GetType().GetProperty("Connection", flags).GetValue(stream, null);
                    var netStream = connection.GetType().GetField("networkStream", flags).GetValue(connection) as Stream;
                    return GetSslProtocol(netStream);
                }

                if (stream.GetType().FullName == "System.Net.TlsStream")
                {
                    var ssl = stream.GetType().GetField("m_Worker", flags).GetValue(stream);
                    if (ssl.GetType().GetProperty("IsAuthenticated", flags).GetValue(ssl, null) as Boolean? != true)
                    {
                        var processAuthMethod = stream.GetType().GetMethod("ProcessAuthentication", flags);
                        processAuthMethod.Invoke(stream, new Object[] { null });
                    }

                    return ssl.GetType().GetProperty("SslProtocol", flags).GetValue(ssl, null) as SslProtocols?;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
