using MercadoPagoCore.Common;

namespace MercadoPagoCore.DataStructures.Plan
{
    public struct AutoRecurring
    {
        public int Frequency { get; set; }
        public string FrequencyType { get; set; }
        public float TransactionAmount { get; set; }
        public CurrencyId CurrencyId { get; set; }
        public int Repetitions { get; set; }
        public int DebitDate { get; set; }
        public FreeTrial FreeTrial { get; set; }
    }
}
