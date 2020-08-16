using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MercadoPagoCore.DataStructures.Customer;
using MercadoPagoCore.Resources;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Resources
{
    [TestFixture(Ignore = "Skipping")]
    public class CustomerTest
    {
        Customer LastCustomer;

        [SetUp]
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            MercadoPagoSDK.CleanConfiguration();
            MercadoPagoSDK.SetBaseUrl("https://api.mercadopago.com");
            MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
        }

        [Test]
        public void Customer_Create_ShouldBeOk()
        {
            Customer customer = new Customer
            {
                FirstName = "Manolo",
                LastName = "Perez",
                Email = "test@test.com",
                Address = new Address
                {
                    StreetName = "Groove Street",
                    ZipCode = "2300"
                },
                Phone = new Phone
                {
                    AreaCode = "03492",
                    Number = "432334"
                },
                Description = "This is a description",
                Identification = new Identification
                {
                    Type = "CC",
                    Number = "123456789"
                }
            };
            customer.Save();
            LastCustomer = customer;

            Assert.IsNotNull(customer.Id, "Failed: Customer could not be successfully created");
        }

        [Test]
        public void Customer_FindById_ShouldBeOk()
        {
            Customer customer = Customer.FindById(LastCustomer.Id);
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.FirstName, LastCustomer.FirstName);
        }

        [Test]
        public void Customer_Update_ShouldBeOk()
        {
            LastCustomer.LastName = "Fernandez";
            LastCustomer.Update();

            Assert.AreEqual("Fernandez", LastCustomer.LastName);
        }

        [Test]
        public void Remove_Customer()
        {
            try
            {
                LastCustomer.Delete();
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Customer_SearchWithFilterGetListOfCustomers()
        {
            Dictionary<string, string> filters = new Dictionary<string, string>()
            {
                {"email", "test@test.com" }
            };
            List<Customer> customers = Customer.Search(filters);

            Assert.IsTrue(customers.Any());
            Assert.IsNotNull(customers.First());
            Assert.AreEqual("test@test.com", customers.First().Email);
        }
    }
}
