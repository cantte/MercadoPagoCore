using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPagoCore.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentTypeId
    {
        /// <summary>Money in the MercadoPago account</summary>
        account_money,
        /// <summary>Printed ticket</summary>
        ticket,
        /// <summary>Wire transfer</summary>
        bank_transfer,
        /// <summary>Payment by ATM</summary>
        atm,
        /// <summary>Payment by credit card</summary>
        credit_card,
        /// <summary>Payment by debit card</summary>
        debit_card,
        /// <summary>Payment by prepaid card</summary>
        prepaid_card,
        /// <summary>Payment by digital currency</summary>
        digital_currency,
        /// <summary>Payment by digital wallet</summary>
        digital_wallet
    }
}
