using System;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class CardToken : MercadoPagoBase
    {
        public string Id { get; set; }
        public string PublicKey { get; set; }
        public string CustomerId { get; set; }
        public string CardId { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdate { get; set; }
        public DateTime? DateDue { get; set; }
        public bool? LuhnValidation { get; set; }
        public bool? LineMode { get; set; }
        public bool? RequireEsc { get; set; }
        public string SecurityCode { get; set; }

        public CardToken Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/v1/card_tokens")]
        public CardToken Save(RequestOptions requestOptions)
        {
            return (CardToken)ProcessMethod<CardToken>("Save", WITHOUT_CACHE, requestOptions);
        }

        public static CardToken FindById(string id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/card_tokens/:id")]
        public static CardToken FindById(string id, bool useCache, RequestOptions requestOptions)
        {
            return (CardToken)ProcessMethod<CardToken>(typeof(CardToken), "FindById", id, useCache, requestOptions);
        }
    }
}
