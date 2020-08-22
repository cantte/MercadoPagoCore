using System;
using System.Collections.Generic;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.DataStructures.Customer;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Resources
{
    public class Customer : MercadoPagoBase
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone? Phone { get; set; }
        public Identification? Identification { get; set; }
        public string DefaultAddress { get; set; }
        public Address? Address { get; set; }
        public DateTime? DateRegistered { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdate { get; set; }
        public JObject Metadata { get; set; }
        public string DefaultCard { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
        public bool? LiveMode { get; set; }

        public static List<Customer> Search(Dictionary<string, string> filters)
        {
            return Search(filters, WITHOUT_CACHE, null);
        }

        /// <summary>
        /// Get all customers acoording to specific filters
        /// </summary>
        [GETEndpoint("/v1/customers/search")]
        public static List<Customer> Search(Dictionary<string, string> filters, bool useCache, RequestOptions requestOptions)
        {
            return (List<Customer>)ProcessMethodBulk<Customer>(typeof(Customer), "Search", filters, useCache, requestOptions);
        }

        /// <summary>
        /// Find a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <param name="useCache">Cache configuration.</param>
        /// <param name="requestOptions">Request options.</param>
        /// <returns>Searched customer.</returns>
        [GETEndpoint("/v1/customers/:id")]
        public static Customer FindById(string id, bool useCache, RequestOptions requestOptions)
        {
            return (Customer)ProcessMethod<Customer>("FindById", id, useCache, requestOptions);
        }

        /// <summary>
        /// Find a customer by ID.
        /// </summary>
        public static Customer FindById(string id)
        {
            return FindById(id, WITHOUT_CACHE, null);
        }

        public Customer Save()
        {
            return Save(null);
        }

        /// <summary>
        /// Save a new customer
        /// </summary>
        [POSTEndpoint("/v1/customers")]
        public Customer Save(RequestOptions requestOptions)
        {
            return (Customer)ProcessMethod<Customer>("Save", WITHOUT_CACHE, requestOptions);
        }

        public Customer Update()
        {
            return Update(null);
        }

        /// <summary>
        /// Update editable properties
        /// </summary>
        [PUTEndpoint("/v1/customers/:id")]
        public Customer Update(RequestOptions requestOptions)
        {
            return (Customer)ProcessMethod<Customer>("Update", WITHOUT_CACHE, requestOptions);
        }

        public Customer Delete()
        {
            return Delete(null);
        }

        /// <summary>
        /// Remove a customer
        /// </summary>
        [DELETEEndpoint("/v1/customers/:id")]
        public Customer Delete(RequestOptions requestOptions)
        {
            return (Customer)ProcessMethod<Customer>("Delete", WITHOUT_CACHE, requestOptions);
        }
    }
}
