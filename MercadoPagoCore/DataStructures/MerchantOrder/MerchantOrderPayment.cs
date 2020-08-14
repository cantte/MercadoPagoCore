using System;
using System.ComponentModel.DataAnnotations;
using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.MerchantOrder
{
    public struct MerchantOrderPayment
    {
        public enum OperationType
        {
            RegularPayment,
            PaymentAddition
        }

        public string ID { get; }
        public float TransactionAmount { get; }
        public float TotalPaidAmount { get; }
        public float ShippingCost { get; }
        [StringLength(3)]
        public CurrencyId PaymentCurrencyId { get; }
        public string Status { get; }
        public string StatusDetail { get; }
        public OperationType PaymentOperationType { get; }
        public DateTime DateApproved { get; }
        public DateTime DateCreated { get; }
        public DateTime LastModified { get; }
        public float AmountRefunded { get; }
    }
}
