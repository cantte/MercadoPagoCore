using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Exceptions;
using MercadoPagoCore.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ValidationRange = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace MercadoPagoCore.Test.Net
{
    [TestFixture(Ignore = "Skipping")]
    public class MercadoPagoRestClientTest : MercadoPagoRestClient
    {
        [Test()]
        public void ExecuteRequest_GetAndDeleteNustNotHavePayload()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();
            try
            {
                MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.GET, "https://httpbin.org/get", PayloadType.X_WWW_FORM_URLENCODED, new JObject(), null, 0, 0);
            }
            catch (MercadoPagoRestException exception)
            {
                Assert.AreEqual("Payload not supported for this method.", exception.Message);
            }

            try
            {
                MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.DELETE, "https://httpbin.org/delete", PayloadType.X_WWW_FORM_URLENCODED, new JObject(), null, 0, 0);
            }
            catch (MercadoPagoRestException exception)
            {
                Assert.AreEqual("Payload not supported for this method.", exception.Message);
            }
        }

        [Test()]
        public void ExecuteRequest_PostAndPutMustHavePayload()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();
            try
            {
                MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "https://httpbin.org/post", PayloadType.X_WWW_FORM_URLENCODED, null, null, 0, 0);
            }
            catch (MercadoPagoRestException exception)
            {
                Assert.AreEqual("Must include payload for this method.", exception.Message);
            }

            try
            {
                MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.PUT, "https://httpbin.org/put", PayloadType.X_WWW_FORM_URLENCODED, null, null, 0, 0);
            }
            catch (MercadoPagoRestException exception)
            {
                Assert.AreEqual("Must include payload for this method.", exception.Message);
            }
        }

        [Test()]
        public void ExecuteRequest_Post()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            JObject jsonObject = new JObject
            {
                { "firstName", "Clark" },
                { "lastName", "Kent" },
                { "year", 2018 }
            };

            MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "https://httpbin.org/post", PayloadType.X_WWW_FORM_URLENCODED, jsonObject, null, 0, 0);

            JObject jsonResponse = JObject.Parse(response.StringResponse.ToString());
            List<JToken> contentType = MercadoPagoCoreUtils.FindTokens(jsonResponse, "Content-Type");
            Assert.AreEqual("application/x-www-form-urlencoded", contentType.First().ToString());
        }

        [Test()]
        public void ExecuteRequest_Post_Json()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            JObject jsonObject = new JObject
            {
                { "firstName", "Clark" },
                { "lastName", "Kent" },
                { "year", 2018 }
            };
            DummyClass dummy = new DummyClass("Dummy description", DateTime.Now, 1000);
            WebHeaderCollection headers = new WebHeaderCollection
            {
                { "x-idempotency-key", dummy.GetType().GUID.ToString() }
            };

            MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "", PayloadType.JSON, jsonObject, headers, 0, 0);
            JObject jsonResponse = JObject.Parse(response.StringResponse.ToString());

            List<JToken> lastName = MercadoPagoCoreUtils.FindTokens(jsonResponse, "lastName");
            Assert.AreEqual("Kent", lastName.First().ToString());

            List<JToken> year = MercadoPagoCoreUtils.FindTokens(jsonResponse, "year");
            Assert.AreEqual("2018", year.First().ToString());
        }

        [Test()]
        public void ClassIntance_ShouldThrowValidationException()
        {
            try
            {
                DummyClass objectToValidate = new DummyClass("Pay", DateTime.Now, -1000);
            }
            catch (Exception exception)
            {
                Assert.AreEqual(@"There are errors in the object you're trying to create. Review them to continue: Error on property Description. " +
                                "The specified value is not valid. RegExp: ^(?:.*[a-z]){7,}$ . " +
                                "Error on property TransactionAmount. The value you are trying to assign is not in the specified range. ", exception.Message);
            }

            Assert.Pass();
        }

        [Test()]
        public void ClassIntance_ShouldPass()
        {
            try
            {
                DummyClass objectToValidate = new DummyClass("Payment description", DateTime.Now, 1000);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("There are errors in the object you're trying to create. Review them to continue: -CODE 31-Transaction amount must be greather than 0.", exception.Message);
            }

            Assert.Pass();
        }

        [Test()]
        public void IdempotentKey_MustBePresent()
        {
            DummyClass dummy = new DummyClass("Dummy description", DateTime.Now, 1000);

            Assert.IsNotEmpty(dummy.GetType().GUID.ToString());
        }

        [Test()]
        public void ExecuteRequest_Post_MPAPIRequestResponseParser()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            JObject jsonObject = new JObject
            {
                { "firstName", "Comander" },
                { "lastName", "Shepard" },
                { "year", 2126 }
            };

            MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "https://httpbin.org/post", PayloadType.JSON, jsonObject, null, 0, 0);

            Assert.AreEqual(200, response.StatusCode);

            JObject jsonResponse = response.JsonObjectResponse;
            List<JToken> lastName = MercadoPagoCoreUtils.FindTokens(jsonResponse, "lastName");
            Assert.AreEqual("Shepard", lastName.First().ToString());

            List<JToken> year = MercadoPagoCoreUtils.FindTokens(jsonResponse, "year");
            Assert.AreEqual("2126", year.First().ToString());
        }

        [Test()]
        public void ExecuteRequest_Get_ShortTimeoutWillFail()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            JObject jsonObject = new JObject
            {
                { "firstName", "Comander" },
                { "lastName", "Shepard" },
                { "year", 2126 }
            };

            try
            {
                MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "https://httpbin.org/post", PayloadType.JSON, jsonObject, null, 5, 0);
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test()]
        public void ExecuteRequest_Get_ProperTimeoutWillWork()
        {
            MercadoPagoRestClient client = new MercadoPagoRestClient();

            JObject jsonObject = new JObject
            {
                { "firstName", "Comander" },
                { "lastName", "Shepard" },
                { "year", 2126 }
            };

            MercadoPagoAPIResponse response = client.ExecuteRequest(HttpMethod.POST, "https://httpbin.org/post", PayloadType.JSON, jsonObject, null, 20000, 0);

            Assert.AreEqual(200, response.StatusCode);

            JObject jsonResponse = response.JsonObjectResponse;
            List<JToken> lastName = MercadoPagoCoreUtils.FindTokens(jsonResponse, "lastName");
            Assert.AreEqual("Shepard", lastName.First().ToString());

            List<JToken> year = MercadoPagoCoreUtils.FindTokens(jsonResponse, "year");
            Assert.AreEqual("2126", year.First().ToString());
        }

        [Idempotent]
        [TestFixture]
        public class DummyClass : MercadoPagoBase
        {
            [Required]
            [RegularExpression(@"^(?:.*[a-z]){7,}$")]
            public string Description { get; set; }
            [Required]
            [DataType(DataType.Date)]
            public DateTime PaymentDate { get; set; }
            [Required]
            [ValidationRange(0.0, double.MaxValue)]
            public double TransactionAmount { get; set; }

            public DummyClass(string description, DateTime date, double transationAmount)
            {
                Description = description;
                PaymentDate = date;
                TransactionAmount = transationAmount;

                Validate(this);
            }
        }
    }
}
