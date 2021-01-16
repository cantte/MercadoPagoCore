# MercadoPagoCore - Repository
This library provides developers with a simple set of bindings to help you integrate Mercado Pago API to a website and start receiving payments.

# Build Status
![.NET Core](https://github.com/cantte/MercadoPagoCore/workflows/.NET%20Core/badge.svg)

# MercadoPagoCore

## 🚀 Package information

[![latest version](https://img.shields.io/nuget/v/MercadoPagoCore)](https://www.nuget.org/packages/MercadoPagoCore)
[![downloads](https://img.shields.io/nuget/dt/MercadoPagoCore)](https://www.nuget.org/packages/MercadoPagoCore)
[![APM](https://img.shields.io/apm/l/vim-mode)](https://github.com/cantte/MercadoPagoCore)


## 💡 Requirements

.net >= 5.0

## 📲 Installation 

### Using our NuGet package

First time using Mercado Pago? Create your [Mercado Pago account](https://www.mercadopago.com), if you don’t have one already.

**Using Package Manager**

`PM> Install-Package MercadoPagoCore -Version 5.0.0`

**Using .Net CLI**

`> dotnet add package MercadoPagoCore --version 5.0.0`

**Using Paket CLI**

`> paket add MercadoPagoCore --version 5.0.0`

**Using Package Reference**

`<PackageReference Include="MercadoPagoCore" Version="5.0.0" />`


Copy the access_token in the [credentials](https://www.mercadopago.com/mlb/account/credentials) section of the page and replace YOUR_ACCESS_TOKEN with it.

That's it! MercadoPagoCore SDK has been successfully installed.

## 🌟 Getting Started

Simple usage looks like:

```csharp
using System;
using System.Threading.Tasks;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

MercadoPagoConfig.AccessToken = "YOUR_ACCESS_TOKEN";

var request = new PaymentCreateRequest
{
    TransactionAmount = 10,
    Token = "CARD_TOKEN",
    Description = "Payment description",
    Installments = 1,
    PaymentMethodId = "visa",
    Payer = new PaymentPayerRequest
    {
        Email = "test.payer@email.com",
    }
};

var client = new PaymentClient();
Payment payment = await client.CreateAsync(request);

Console.WriteLine($"Payment ID: {payment.Id}");
```

## 📚 Documentation 

Visit our Dev Site for further information regarding:
 - Payments APIs: [Spanish](https://www.mercadopago.com.ar/developers/es/guides/payments/api/introduction/) / [Portuguese](https://www.mercadopago.com.br/developers/pt/guides/payments/api/introduction/)
 - Mercado Pago checkout: [Spanish](https://www.mercadopago.com.ar/developers/es/guides/payments/web-payment-checkout/introduction/) / [Portuguese](https://www.mercadopago.com.br/developers/pt/guides/payments/web-payment-checkout/introduction/)
 - Web Tokenize checkout: [Spanish](https://www.mercadopago.com.ar/developers/es/guides/payments/web-tokenize-checkout/introduction/) / [Portuguese](https://www.mercadopago.com.br/developers/pt/guides/payments/web-tokenize-checkout/introduction/)


## ❤️ Support 

If you require technical support, please contact our support team at [developers.mercadopago.com](https://developers.mercadopago.com)

## 🏻 License 

```
MIT license. Copyright (c) 2020 
For more information, see the LICENSE file.
```
