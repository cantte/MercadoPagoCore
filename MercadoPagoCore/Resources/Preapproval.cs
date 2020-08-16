using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MercadoPagoCore.Core;
using MercadoPagoCore.DataStructures.Preapproval;
using MercadoPagoCore.Net;

namespace MercadoPagoCore.Resources
{
    public class Preapproval : MercadoPagoBase
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        [StringLength(256)]
        public string PayerEmail { get; set; }
        [StringLength(500)]
        public string BackUrl { get; set; }
        public string InitPoint { get; set; }
        public string SandboxInitPoint { get; set; }
        public AutoRecurring? AutoRecurring { get; set; }
        [StringLength(256)]
        public string ExternalReference { get; set; }

        public static Preapproval FindById(string id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/preapproval/:id")]
        public static Preapproval FindById(string id, bool useCache, RequestOptions requestOptions)
        {
            return (Preapproval)ProcessMethod<Preapproval>(typeof(Preapproval), "FindById", id, useCache, requestOptions);
        }

        public bool Save()
        {
            return Save(null);
        }

        [POSTEndpoint("/preapproval")]
        public bool Save(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Preapproval>("Save", WITHOUT_CACHE, requestOptions);
        }

        public bool Update()
        {
            return Update(null);
        }

        [PUTEndpoint("/preapproval/:id")]
        public bool Update(RequestOptions requestOptions)
        {
            return ProcessMethodBool<Preapproval>("Update", WITHOUT_CACHE, requestOptions);
        }

        public static List<Preapproval> Search(Dictionary<string, string> filters)
        {
            return Search(filters, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/preapproval/search")]
        public static List<Preapproval> Search(Dictionary<string, string> filters, bool useCache, RequestOptions requestOptions)
        {
            return ProcessMethodBulk<Preapproval>(typeof(Preapproval), "Search", filters, useCache, requestOptions);
        }
    }
}
