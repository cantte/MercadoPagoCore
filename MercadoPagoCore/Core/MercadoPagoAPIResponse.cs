using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using MercadoPagoCore.Exceptions;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Core
{
    public class MercadoPagoAPIResponse
    {
        public MercadoPagoAPIResponse(HttpMethod httpMethod, HttpWebRequest request, JObject payload, HttpWebResponse response)
        {
            Request = request;
            Response = response;

            ParseRequest(httpMethod, request, payload);
            ParseResponse(response);

            Trace.WriteLine("Server response: " + StringResponse);
        }

        private void ParseRequest(HttpMethod httpMethod, HttpWebRequest request, JObject payload)
        {
            HttpMethod = httpMethod.ToString();
            Url = request.RequestUri.ToString();

            if (payload != null)
                Payload = payload.ToString();
        }

        private void ParseResponse(HttpWebResponse response)
        {
            StatusCode = (int)response.StatusCode;
            StatusDescription = response.StatusDescription;

            using Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                try
                {
                    using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    StringResponse = reader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw new MercadoPagoException(ex.Message);
                }

                try
                {
                    JsonObjectResponse = JObject.Parse(StringResponse);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error parsing jsonObect");
                }
            }
        }

        public string HttpMethod { get; protected set; }
        public string Url { get; protected set; }
        public HttpWebRequest Request { get; protected set; }
        public string Payload { get; protected set; }
        public HttpWebResponse Response { get; protected set; }

        public int StatusCode { get; protected set; }
        public string StatusDescription { get; protected set; }

        public string StringResponse { get; protected set; }
        public JObject JsonObjectResponse { get; protected set; }

        public bool IsFromCache = false;
    }
}
