using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using MercadoPagoCore.Core;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Exceptions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MercadoPagoCore.Test.Core
{
    [TestFixture(Ignore = "Skipping")]
    public class MercadoPagoBaseTest : MercadoPagoBase
    {
        [SetUp]
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            LaunchSettingsFixture.LoadLaunchSettings();
            MercadoPagoSDK.ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            MercadoPagoSDK.ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        }

        public static MercadoPagoBaseTest FindById(string id, bool useCache)
        {
            return (MercadoPagoBaseTest)ProcessMethod<MercadoPagoBaseTest>("FindById", id, useCache);
        }

        [GETEndpoint("/v1/getpath/slug")]
        public static List<MercadoPagoBaseTest> All()
        {
            return ProcessMethodBulk<MercadoPagoBaseTest>(typeof(MercadoPagoBase), "All", false);
        }

        [Test()]
        public void MercadoPagoBaseTest_WithNoAttributes_ShouldraiseException()
        {
            try
            {
                MercadoPagoBaseTest result = FindById("666", false);
                Assert.Fail();
            }
            catch (MercadoPagoException exception)
            {
                Assert.AreEqual("No annotated method found", exception.Message);
            }
        }

        [Test()]
        public void MPBaseTest_WithAttributes_ShouldFindAttribute()
        {
            try
            {
                List<MercadoPagoBaseTest> result = All();
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Idempotent]
        [TestFixture(Ignore = "Skipping")]
        [UserToken("as987ge9ev6s5df4g32z1xv54654")]
        public class DummyClass : MercadoPagoBase
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string MaritalStatus { get; set; }
            public bool HasCreditCard { get; set; }

            public static DummyClass All()
            {
                return (DummyClass)ProcessMethod("All", false);
            }

            [GETEndpoint("/get/:id")]
            public static DummyClass FindById(string id, bool useCache)
            {
                return (DummyClass)ProcessMethod<DummyClass>("FindById", id, useCache);
            }

            [POSTEndpoint("/post")]
            public DummyClass Save()
            {
                return (DummyClass)ProcessMethod<DummyClass>("Save", false);
            }

            [PUTEndpoint("/put")]
            public DummyClass Update()
            {
                return (DummyClass)ProcessMethod<DummyClass>("Update", false);
            }

            // Cache Test
            [Test()]
            public void DummyClassMethod_RequestMustBeCachedButNotRetrievedFromCache()
            {
                ResetConfiguration();

                string id = new Random().Next(0, int.MaxValue).ToString();


                DummyClass firstResult = FindById(id, true);
                Assert.IsFalse(firstResult.GetLastApiResponse().IsFromCache);
            }

            private void ResetConfiguration()
            {
                MercadoPagoSDK.CleanConfiguration();
                MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
                MercadoPagoSDK.SetBaseUrl("https://httpbin.org");
            }

            [Test()]
            public void DummyClassMethod_RequestMustBeRetrievedFromCache()
            {
                ResetConfiguration();

                string id = new Random().Next(0, int.MaxValue).ToString();

                DummyClass firstResult = FindById(id, true);

                Thread.Sleep(1000);

                DummyClass cachedResult = FindById(id, true);
                Assert.IsTrue(cachedResult.GetLastApiResponse().IsFromCache);
            }

            [Test()]
            public void DummyClassMethod_RequestMustBeRetrievedFromCacheButItsNotThere()
            {
                ResetConfiguration();

                Random random = new Random();
                string id1 = (random.Next(0, int.MaxValue) - 78).ToString();
                string id2 = (random.Next(0, int.MaxValue) - 3).ToString();

                DummyClass firstResult = FindById(id1, true);

                Thread.Sleep(1000);

                DummyClass notRetrievedFromCacheResult = FindById(id2, true);

                Assert.IsFalse(notRetrievedFromCacheResult.GetLastApiResponse().IsFromCache);
            }

            [Test()]
            public void DummyClassMethod_SeveralRequestsMustBeCached()
            {
                ResetConfiguration();

                Random random = new Random();
                string id1 = (random.Next(0, int.MaxValue) - 5).ToString();
                string id2 = (random.Next(0, int.MaxValue) - 88).ToString();
                string id3 = (random.Next(0, int.MaxValue) - 9).ToString();

                DummyClass firstResult = FindById(id1, true);
                DummyClass secondResult = DummyClass.FindById(id2, true);
                DummyClass thirdResult = DummyClass.FindById(id3, true);

                Thread.Sleep(1000);

                DummyClass firstCachedResult = DummyClass.FindById(id1, true);
                DummyClass secondCachedResult = DummyClass.FindById(id2, true);
                DummyClass thirdCachedResult = DummyClass.FindById(id3, true);

                Assert.IsTrue(firstCachedResult.GetLastApiResponse().IsFromCache);
                Assert.IsTrue(secondCachedResult.GetLastApiResponse().IsFromCache);
                Assert.IsTrue(thirdCachedResult.GetLastApiResponse().IsFromCache);
            }

            [Test()]
            public void DummyClassMethod_SeveralRequestAreNotRetrievedFromCacheInFirstAttempt()
            {
                ResetConfiguration();

                Random random = new Random();
                string id1 = (random.Next(0, int.MaxValue) - 15).ToString();
                string id2 = (random.Next(0, int.MaxValue) - 666).ToString();
                string id3 = (random.Next(0, int.MaxValue) - 71).ToString();

                DummyClass firstResult = FindById(id1, true);
                DummyClass secondResult = DummyClass.FindById(id2, true);
                DummyClass thirdResult = DummyClass.FindById(id3, true);

                Assert.IsFalse(firstResult.GetLastApiResponse().IsFromCache);
                Assert.IsFalse(secondResult.GetLastApiResponse().IsFromCache);
                Assert.IsFalse(thirdResult.GetLastApiResponse().IsFromCache);
            }

            [Test()]
            public void AddToCache_ShouldExecuteWithoutProblems()
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://httpbin.org?mock=12345");
                    MercadoPagoCache.AddToCache("NewCache", new MercadoPagoAPIResponse(HttpMethod.GET, request, new JObject() { }, (HttpWebResponse)request.GetResponse()));
                }
                catch (MercadoPagoException)
                {
                    Assert.Fail();
                }

                Assert.Pass();
            }

            // End cache test

            [Test()]
            public void GetAccessToken_ShouldThrowException()
            {
                try
                {
                    MercadoPagoSDK.CleanConfiguration();
                    MercadoPagoCredentials.GetAccessToken();
                }
                catch (MercadoPagoException exception)
                {
                    Assert.AreEqual("\"client_id\" and \"client_secret\" can not be \"null\" when getting the \"access_token\"", exception.Message);
                    return;
                }
            }

            [Test()]
            public void DummyClassMethod_WithNoAttributes_ShouldraiseException()
            {
                try
                {
                    var result = All();
                }
                catch (MercadoPagoException exception)
                {
                    Assert.AreEqual("No annotated method found", exception.Message);
                    return;
                }

                Assert.Fail();
            }

            [Test()]
            public void DummyClassMethod_WitAttributes_CreateNonStaticMethodShouldFindAttribute()
            {
                Dictionary<string, string> config = new Dictionary<string, string>
                {
                    { "clientSecret", Environment.GetEnvironmentVariable("CLIENT_SECRET") },
                    { "clientId", Environment.GetEnvironmentVariable("CLIENT_ID") },
                    { "accessToken", Environment.GetEnvironmentVariable("ACCESS_TOKEN") }
                };

                MercadoPagoSDK.SetConfiguration(config);

                DummyClass resource = new DummyClass();
                try
                {
                    DummyClass result = resource.Save();
                }
                catch (Exception)
                {
                    Assert.Fail();
                    return;
                }

                Assert.Pass();
            }

            [Test()]
            public void DummyClassMethod_Create_CheckUri()
            {
                ResetConfiguration();

                DummyClass resource = new DummyClass
                {
                    Address = "Evergreen 123",
                    Email = "fake@email.com"
                };
                DummyClass result;
                try
                {
                    result = resource.Save();
                }
                catch (Exception)
                {
                    Assert.Fail();
                    return;
                }

                Assert.AreEqual("POST", result.GetLastApiResponse().HttpMethod);

                Assert.AreEqual($"https://httpbin.org/post?access_token={Environment.GetEnvironmentVariable("ACCESS_TOKEN")}", result.GetLastApiResponse().Url);
            }

            [Test()]
            public void DummyClassMethod_Update_CheckUri()
            {
                ResetConfiguration();
                DummyClass resource = new DummyClass
                {
                    Address = "Evergreen 123",
                    Email = "fake@email.com"
                };
                DummyClass result;
                try
                {
                    result = resource.Update();
                }
                catch (Exception)
                {
                    Assert.Fail();
                    return;
                }

                Assert.AreEqual("PUT", result.GetLastApiResponse().HttpMethod);
                Assert.AreEqual($"https://httpbin.org/post?access_token={Environment.GetEnvironmentVariable("ACCESS_TOKEN")}", result.GetLastApiResponse().Url);
            }

            [Test()]
            public void DummyClassMethod_WithoutClassReference()
            {
                try
                {
                    var result = All();
                }
                catch (MercadoPagoException exception)
                {
                    Assert.AreEqual("No annotated method found", exception.Message);
                    return;
                }

                Assert.Fail();
            }
        }

        [TestFixture(Ignore = "Skipping")]
        [UserToken("as987ge9ev6s5df4g32z1xv54654")]
        public class AnotherDummyClass : MercadoPagoBase
        {
            [PUTEndpoint("")]
            public AnotherDummyClass Update()
            {
                return (AnotherDummyClass)ProcessMethod("Update", false);
            }

            [Test()]
            public void AnotherDummyClass_EmptyEndPoint_ShouldRaiseExcep()
            {
                AnotherDummyClass resource = new AnotherDummyClass();
                AnotherDummyClass result;
                try
                {
                    result = resource.Update();
                }
                catch (MercadoPagoException exception)
                {
                    Assert.AreEqual("Path not found for PUT method", exception.Message);
                    return;
                }

                Assert.Fail();
            }

            [Test()]
            public void MPBase_ParsePath_ShouldReplaceParamInUrlWithValues()
            {
                MercadoPagoSDK.CleanConfiguration();
                MercadoPagoSDK.AccessToken = "as987ge9ev6s5df4g32z1xv54654";

                DummyClass dummy = new DummyClass
                {
                    Id = 111,
                    Email = "person@something.com",
                    Address = "Evergreen123",
                    MaritalStatus = "divorced",
                    HasCreditCard = true
                };

                try
                {
                    string processedPath = ParsePath("https://api.mercadopago.com/v1/getpath/slug/:id/pUnexist/:unexist", null, dummy);
                }
                catch (Exception exception)
                {
                    Assert.AreEqual("No argument supplied/found for method path", exception.Message);
                }

                string processedPath0 = ParsePath("/v1/getpath/slug", null, dummy);
                Assert.AreEqual("https://api.mercadopago.com/v1/getpath/slug?access_token=as987ge9ev6s5df4g32z1xv54654", processedPath0);

                string processedPath1 = ParsePath("/v1/putpath/slug/:id/pEmail/:email", null, dummy);
                Assert.AreEqual("https://api.mercadopago.com/v1/putpath/slug/111/pEmail/person@something.com?access_token=as987ge9ev6s5df4g32z1xv54654", processedPath1);

                string processedPath2 = ParsePath("/v1/putpath/slug/:id/pHasCreditCard/:hasCreditCard", null, dummy);
                Assert.AreEqual("https://api.mercadopago.com/v1/putpath/slug/111/pHasCreditCard/True?access_token=as987ge9ev6s5df4g32z1xv54654", processedPath2);

                string processedPath3 = ParsePath("/v1/putpath/slug/:id/pEmail/:email/pAddress/:address", null, dummy);
                Assert.AreEqual("https://api.mercadopago.com/v1/putpath/slug/111/pEmail/person@something.com/pAddress/Evergreen123?access_token=as987ge9ev6s5df4g32z1xv54654", processedPath3);

                string processedPath4 = ParsePath("/v1/putpath/slug/:id/pEmail/:email/pAddress/:address/pMaritalstatus/:maritalStatus/pHasCreditCard/:hasCreditCard", null, dummy);
                Assert.AreEqual("https://api.mercadopago.com/v1/putpath/slug/111/pEmail/person@something.com/pAddress/Evergreen123/pMaritalstatus/divorced/pHasCreditCard/True?access_token=as987ge9ev6s5df4g32z1xv54654", processedPath4);
            }
        }

        [TestFixture(Ignore = "Skipping")]
        [UserToken("as987ge9ev6s5df4g32z1xv54654")]
        public class ResourceTestClass : MercadoPagoBase
        {
            public string CardNumber { get; set; }
            public string Holder { get; set; }

            [GETEndpoint("/getpath/load/:id", requestTimeout: 5000, retries: 3)]
            public ResourceTestClass Load(string id)
            {
                return (ResourceTestClass)ProcessMethod<ResourceTestClass>("FindById", id, false);
            }

            [POSTEndpoint("/post", requestTimeout: 6000, retries: 0)]
            public ResourceTestClass Save()
            {
                return (ResourceTestClass)ProcessMethod<ResourceTestClass>("Save", false);
            }

            [Test()]
            public void CustomerTestClass_Load_TimeoutFail()
            {
                MercadoPagoSDK.CleanConfiguration();
                MercadoPagoSDK.SetBaseUrl("https://api.mercadopago.com");

                Dictionary<string, string> config = new Dictionary<string, string>
                {
                    { "clientSecret", Environment.GetEnvironmentVariable("CLIENT_SECRET") },
                    { "clientId", Environment.GetEnvironmentVariable("CLIENT_ID") }
                };
                MercadoPagoSDK.SetConfiguration(config);

                ResourceTestClass resource = new ResourceTestClass();
                ResourceTestClass result;
                try
                {
                    result = resource.Load("567");
                }
                catch (Exception)
                {
                    Assert.Pass();
                    return;
                }

                Assert.Fail();
            }

            [Test()]
            public void ResourceTestClass_Create_ProperTimeoutSuccess()
            {
                MercadoPagoSDK.CleanConfiguration();
                MercadoPagoSDK.SetBaseUrl("https://httpbin.org");
                MercadoPagoSDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");

                ResourceTestClass resource = new ResourceTestClass
                {
                    CardNumber = "123456789",
                    Holder = "Wayne"
                };
                ResourceTestClass result;
                try
                {
                    result = resource.Save();
                }
                catch (Exception)
                {
                    Assert.Fail();
                    return;
                }

                JObject jsonResponse = result.GetJsonSource();
                List<JToken> lastName = MercadoPagoCoreUtils.FindTokens(jsonResponse, "CardNumber");
                Assert.AreEqual("123456789", lastName.First().ToString());

                List<JToken> year = MercadoPagoCoreUtils.FindTokens(jsonResponse, "Holder");
                Assert.AreEqual("Wayne", year.First().ToString());
            }
        }
    }
}
