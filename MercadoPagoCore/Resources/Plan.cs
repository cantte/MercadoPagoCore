using System;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.Plan;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class Plan : MercadoPagoBase
    {
        public string Id { get; set; }
        public float Application_fee { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string External_reference { get; set; }
        public DateTime? Date_created { get; set; }
        public DateTime? Last_modified { get; set; }
        public AutoRecurring Auto_recurring { get; set; }
        public bool Live_mode { get; set; }
        public float Setup_fee { get; set; }
        public string Metadata { get; set; }

        public static Plan Load(string id)
        {
            return Load(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/v1/plans/:id")]
        public static Plan Load(string id, bool useCache, RequestOptions requestOptions)
        {
            return (Plan)ProcessMethod<Plan>(typeof(Plan), "Load", id, useCache, requestOptions);
        }

        public Plan Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/v1/plans")]
        public Plan Save(RequestOptions requestOptions)
        {
            return (Plan)ProcessMethod<Plan>("Save", WITHOUT_CACHE, requestOptions);
        }

        public Plan Update()
        {
            return Update(null);
        }

        [PUTEndpoint("/v1/plans/:id")]
        public Plan Update(RequestOptions requestOptions)
        {
            return (Plan)ProcessMethod<Plan>("Update", WITHOUT_CACHE, requestOptions);
        }
    }
}
