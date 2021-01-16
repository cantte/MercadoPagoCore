using System.Collections.Generic;

namespace MercadoPagoCore.Error
{
    public class ApiError
    {
        public int? Status { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public List<ApiErrorCause> Cause { get; set; }
    }
}
