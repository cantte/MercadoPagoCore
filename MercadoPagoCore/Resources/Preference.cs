using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MercadoPagoCore.Common;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.DataStructures.Preference;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Resources
{
    public class Preference : MercadoPagoBase
    {
        public Payer? Payer { get; set; }
        public PaymentMethods? PaymentMethods { get; set; }
        public Shipment? Shipments { get; set; }
        public BackUrls? BackUrls { get; set; }
        [StringLength(500)]
        public string NotificationUrl { get; set; }
        public string Id { get; set; }
        public string InitPoint { get; set; }
        public string SandboxInitPoint { get; set; }
        public string Purpose { get; set; }
        public DateTime? Datecreated { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType? OperationType { get; set; }
        public string AdditionalInfo { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AutoReturnType? AutoReturn { get; set; }
        [StringLength(256)]
        public string ExternalReference { get; set; }
        public bool? Expires { get; set; }
        public DateTime? ExpirationDateFrom { get; set; }
        public DateTime? ExpirationDateTo { get; set; }
        public int? CollectorId { get; set; }
        public string ClientId { get; set; }
        [StringLength(256)]
        public string Marketplace { get; set; }
        public float? Marketplace_fee { get; set; }
        public DifferentialPricing? Differential_pricing { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public long? SponsorId { get; set; }
        public List<ProcessingMode> ProcessingModes { get; set; } = new List<ProcessingMode>();
        public bool? BinaryMode { get; set; }
        public List<Tax> Taxes { get; set; }
        public JObject Metadata { get; set; }
        public List<Track> Tracks { get; set; }

        public static Preference FindById(string id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/checkout/preferences/:id")]
        public static Preference FindById(string id, bool useCache, RequestOptions requestOptions)
        {
            return (Preference)ProcessMethod<Preference>(typeof(Preference), "FindById", id, useCache, requestOptions);
        }

        public bool Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/checkout/preferences")]
        public bool Save(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Preference>("Save", WITHOUT_CACHE, requestOptions);
        }

        public bool Update()
        {
            return Update(null);
        }

        [PUTEndpoint("/checkout/preferences/:id")]
        public bool Update(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Preference>("Update", WITHOUT_CACHE, requestOptions);
        }
    }
}
