using System;
using System.Collections.Generic;
using MercadoPagoCore.DataStructures.Payment;
using MercadoPagoCore.Resources;

namespace MercadoPagoCore.Test.Helpers
{
    public static class PaymentHelper
    {
        public static Payment GetPaymentData(string publicKey, string status)
        {
            Address addInfPayerAdd = new Address
            {
                StreetName = "Street",
                StreetNumber = 5,
                ZipCode = "54321"
            };

            Phone addInfPayerPhone = new Phone
            {
                AreaCode = "00",
                Number = "5512345678"
            };

            DateTime date = new DateTime(2000, 01, 31);

            AdditionalInfoPayer addInfoPayer = new AdditionalInfoPayer
            {
                FirstName = "Manolo",
                LastName = "Perez",
                RegistrationDate = date,
                Address = addInfPayerAdd,
                Phone = addInfPayerPhone
            };

            Item item = new Item
            {
                Id = "123",
                Title = "Celular blanco",
                Description = "4G, 32GB",
                Quantity = 1,
                PictureUrl = "http://www.imagenes.com/celular.jpg",
                UnitPrice = 100.4m
            };
            List<Item> items = new List<Item>
            {
                item
            };

            ReceiverAddress receiverAddress = new ReceiverAddress
            {
                StreetName = "Insurgentes sur",
                StreetNumber = 1,
                Zip_code = "12345"
            };

            Shipment shipment = new Shipment
            {
                ReceiverAddress = receiverAddress
            };

            AdditionalInfo addInf = new AdditionalInfo
            {
                Payer = addInfoPayer,
                Shipments = shipment,
                Items = items

            };

            Payment payment = new Payment
            {
                TransactionAmount = 50000f,
                Token = CardHelper.SingleUseCardToken(publicKey, status),
                Description = "Pago de prueba",
                PaymentMethodId = "visa",
                ExternalReference = "",
                Installments = 1,
                Payer = new Payer
                {
                    Email = "test@test.com"
                },
                AdditionalInfo = addInf
            };

            return payment;
        }
    }
}
