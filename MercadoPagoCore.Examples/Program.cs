using MercadoPagoCore.DataStructures.Payment;
using MercadoPagoCore.Resources;

namespace MercadoPagoCore.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            MercadoPagoSDK.AccessToken = "TEST-2823460721006149-081117-77b75c2b5a55f9b50c63417d40210f45-251462384";

            Payment payment = new Payment
            {
                TransactionAmount = float.Parse("90000"),
                Token = "534e4aab1346057efa768ad50609fba4",
                Description = "Cats",
                PaymentMethodId = "visa",
                Installments = 1,
                Payer = new Payer
                {
                    Email = "test@test.com"
                },
            };

            if (payment.Save())
            {
                System.Console.WriteLine(payment.Status);
            }
        }
    }
}
