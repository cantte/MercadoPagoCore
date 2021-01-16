namespace MercadoPagoCore.Tests.Client.Helper
{
    using MercadoPagoCore.Resource.User;

    public static class CurrencyHelper
    {
        public static string GetCurrencyId(User user)
        {
            switch (user?.SiteId)
            {
                case "MLA":
                    return "ARS";
                case "MLB":
                    return "BRL";
                case "MLM":
                    return "MXN";
                case "MCO":
                    return "COP";
                case "MLC":
                    return "CLP";
                case "MLU":
                    return "UYU";
                case "MPE":
                    return "PEN";
                default:
                    return string.Empty;
            }
        }
    }
}
