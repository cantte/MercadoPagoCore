using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MercadoPagoCore.Exceptions
{
    [Serializable]
    public class MercadoPagoException : Exception
    {
        public MercadoPagoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorMessage = info.GetString("ErrorMessage");
        }

        public MercadoPagoException(string message) : base(message)
        {
        }

        public MercadoPagoException(string message, Exception exception) : base(message, exception)
        {
        }

        public MercadoPagoException(string message, string requestId, int? statusCode) : base(message)
        {
            RequestId = requestId;
            StatusCode = statusCode;
        }

        public MercadoPagoException(string message, string requestId, int statusCode, Exception exception) : base(message, exception)
        {
            RequestId = requestId;
            StatusCode = statusCode;
        }

        public MercadoPagoException(int statusCode, string message, List<string> cause) : base(message)
        {
            StatusCode = statusCode;
            Cause = cause;
        }

        public MercadoPagoException()
        {
        }

        public string RequestId { get; set; }
        public int? StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Error { get; set; }
        public List<string> Cause { get; set; } = new List<string>();

        public override string ToString()
        {
            string requestId = string.Empty;
            if (!string.IsNullOrEmpty(RequestId))
                requestId = $"; request-id: { RequestId }";

            string statusCode = string.Empty;
            if (StatusCode.HasValue)
                statusCode = $"; status_code: { StatusCode.Value }";

            return base.ToString() + $"{requestId}{statusCode}";
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RequestId", RequestId);
            info.AddValue("StatusCode", StatusCode);
        }
    }
}
