using System;
using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.Customer.Card;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class Card : MercadoPagoBase
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string FirstSixDigits { get; set; }
        public string LastFourDigits { get; set; }
        public CardPaymentMethod? PaymentMethod { get; set; }
        public SecurityCode? SecurityCode { get; set; }
        public Issuer? Issuer { get; set; }
        public CardHolder? CardHolder { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public string Token { get; set; }

        public static List<Card> All(string customerId)
        {
            return All(customerId, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/customers/:customer_id/cards")]
        public static List<Card> All(string customerId, bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<Card>(typeof(Card), "All", customerId, useCache, requestOptions);
        }

        public static Card FindById(string customerId, string id)
        {
            return FindById(customerId, id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/customers/:customer_id/cards/:id")]
        public static Card FindById(string customerId, string id, bool useCache, RequestOptions requestOptions)
        {
            return (Card)ProcessMethod<Card>(typeof(Card), "FindById", customerId, id, useCache, requestOptions);
        }

        public Card Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/v1/customers/:customer_id/cards/")]
        public Card Save(RequestOptions requestOptions)
        {
            return (Card)ProcessMethod<Card>("Save", WITHOUT_CACHE, requestOptions);
        }

        public Card Update()
        {
            return Update(null);
        }

        [PUTEndpoint("/v1/customers/:customer_id/cards/:id")]
        public Card Update(RequestOptions requestOptions)
        {
            return (Card)ProcessMethod<Card>("Update", WITHOUT_CACHE, requestOptions);
        }

        public Card Delete()
        {
            return Delete(null);
        }

        [DELETEEndpoint("/v1/customers/:customer_id/cards/:id")]
        public Card Delete(RequestOptions requestOptions)
        {
            return (Card)ProcessMethod<Card>("Delete", WITHOUT_CACHE, requestOptions);
        }
    }
}
