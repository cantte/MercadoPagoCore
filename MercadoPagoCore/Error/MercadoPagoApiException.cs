using System;
using System.Text;
using MercadoPagoCore.Http;

namespace MercadoPagoCore.Error
{
    public class MercadoPagoApiException : Exception
    {
        public MercadoPagoApiException(string message, MercadoPagoResponse response)
            : base(message)
        {
            ApiResponse = response;
            StatusCode = response?.StatusCode;
        }

        public int? StatusCode { get; }
        public ApiError ApiError { get; set; }
        public MercadoPagoResponse ApiResponse { get; }
        public override string Message
        {
            get
            {
                var messageSb = new StringBuilder();
                messageSb.Append(base.Message);
                if (StatusCode.HasValue)
                {
                    messageSb.Append($" | Status code: {StatusCode}");
                }
                if (!string.IsNullOrWhiteSpace(ApiError?.Message))
                {
                    messageSb.Append($" | API message: {ApiError.Message}");
                }
                return messageSb.ToString();
            }
        }
    }
}
