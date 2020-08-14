using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.MerchantOrder;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class MerchantOrder : MercadoPagoBase
    {
        public string ID { get; }
        public string PreferenceId { get; set; }
        public DateTime? DateCreated { get; }
        public DateTime? LastUpdate { get; }
        public string ApplicationId { get; set; }
        public string Status { get; }
        public string SiteId { get; set; }
        public Payer Payer { get; set; }
        public Collector Collector { get; set; }
        public long? SponsorId { get; set; }
        public List<MerchantOrderPayment> Payments { get; }
        public float? PaidAmount { get; }
        public float? RefundedAmount { get; }
        public float? ShippingCost { get; }
        public bool? Cancelled { get; set; }
        public List<Item> Items { get; set; }
        public List<Shipment> Shipments { get; set; }
        [StringLength(500)]
        public string NotificationUrl { get; set; }
        [StringLength(600)]
        public string AdditionalInfo { get; set; }
        [StringLength(256)]
        public string ExternalReference { get; set; }
        [StringLength(256)]
        public string Marketplace { get; set; }
        public float? TotalAmount { get; }



        public void AppendItem(Item item)
        {
            if (Items == null)
                Items = new List<Item>();
            Items.Add(item);
        }

        public void AppendShipment(Shipment shipment)
        {
            if (Shipments == null)
                Shipments = new List<Shipment>();
            Shipments.Add(shipment);
        }

        public MerchantOrder Load(string id)
        {
            return Load(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/merchant_orders/:id")]
        public MerchantOrder Load(string id, bool useCache, RequestOptions requestOptions)
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>(typeof(MerchantOrder), "Load", id, useCache, requestOptions);
        }

        public MerchantOrder Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/merchant_orders")]
        public MerchantOrder Save(RequestOptions requestOptions)
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>("Save", WITHOUT_CACHE, requestOptions);
        }

        public MerchantOrder Update()
        {
            return Update(null);
        }

        [PUTEndpoint("/merchant_orders/:id")]
        public MerchantOrder Update(RequestOptions requestOptions)
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>("Update", WITHOUT_CACHE, requestOptions);
        }
    }
}
