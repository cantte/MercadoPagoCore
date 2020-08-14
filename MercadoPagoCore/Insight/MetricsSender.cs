using System;
using System.Net;
using System.Security.Authentication;
using System.Threading;

namespace MercadoPagoCore.Insight
{
    public class MetricsSender
    {
        private readonly HttpWebRequest _request;
        private readonly HttpWebResponse _response;
        private readonly SslProtocols? _sslProtocol;
        private readonly int _retries;
        private readonly DateTime _start;
        private readonly DateTime _startRequest;
        private readonly DateTime _endRequest;

        public MetricsSender(HttpWebRequest request, HttpWebResponse response, SslProtocols? sslProtocols, int retries, DateTime start, DateTime startRequest, DateTime endRequest)
        {
            _request = request;
            _response = response;
            _sslProtocol = sslProtocols;
            _retries = retries;
            _start = start;
            _startRequest = startRequest;
            _endRequest = endRequest;
        }

        public void Send()
        {
            try
            {
                new Thread(() =>
                {
                    if (InsightDataManager.Instance.IsInsightMetricsEnabled(_request.Address.ToString()))
                    {
                        InsightDataManager.Instance.SendInsightMetrics(_request, _response, _sslProtocol, _retries, _start, _startRequest, _endRequest);
                    }
                }).Start();
            }
            catch (Exception)
            {
                // Do nothing
            }
        }
    }
}
