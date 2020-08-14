namespace MercadoPagoCore.DataStructures.Generic
{
    public struct BadParamsError
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public int Status { get; set; }
        public BadParamsCause[] Cause { get; set; }
    }
}
