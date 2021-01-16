namespace MercadoPagoCore.Tests.Client.PreApproval
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MercadoPagoCore.Client;
    using MercadoPagoCore.Client.PreApproval;
    using MercadoPagoCore.Config;
    using MercadoPagoCore.Http;
    using MercadoPagoCore.Resource;
    using MercadoPagoCore.Resource.PreApproval;
    using MercadoPagoCore.Serialization;
    using MercadoPagoCore.Tests.Client.Helper;
    using Xunit;

    [Collection("Uses User Email")]
    public class PreApprovalClientTest : BaseClientTest
    {
        private readonly PreApprovalClient client;

        public PreApprovalClientTest(ClientFixture clientFixture)
            : base(clientFixture)
        {
            client = new PreApprovalClient();
        }

        [Fact]
        public void Constructor_HttpClientAndSerializer_Success()
        {
            var httpClient = new DefaultHttpClient();
            var serializer = new DefaultSerializer();
            var client = new PreApprovalClient(httpClient, serializer);

            Assert.Equal(httpClient, client.HttpClient);
            Assert.Equal(serializer, client.Serializer);
        }

        [Fact]
        public void Constructor_HttpClient_Success()
        {
            var httpClient = new DefaultHttpClient();
            var client = new PreApprovalClient(httpClient);

            Assert.Equal(httpClient, client.HttpClient);
            Assert.Equal(MercadoPagoConfig.Serializer, client.Serializer);
        }

        [Fact]
        public void Constructor_Serializer_Success()
        {
            var serializer = new DefaultSerializer();
            var client = new PreApprovalClient(serializer);

            Assert.Equal(MercadoPagoConfig.HttpClient, client.HttpClient);
            Assert.Equal(serializer, client.Serializer);
        }

        [Fact]
        public void Constructor_NullParameters_Success()
        {
            var client = new PreApprovalClient();

            Assert.Equal(MercadoPagoConfig.HttpClient, client.HttpClient);
            Assert.Equal(MercadoPagoConfig.Serializer, client.Serializer);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();

            PreApproval preapproval = await client.CreateAsync(request);

            Assert.NotNull(preapproval);
            Assert.NotNull(preapproval.Id);
        }

        [Fact]
        public void Create_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();

            PreApproval preapproval = client.Create(request);

            Assert.NotNull(preapproval);
            Assert.NotNull(preapproval.Id);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = await client.CreateAsync(request);

            await Task.Delay(1000);

            PreApprovalUpdateRequest updateRequest = BuildUpdateRequest();
            PreApproval preapproval =
                await client.UpdateAsync(createdPreApproval.Id, updateRequest);

            Assert.NotNull(preapproval);
            Assert.Equal(updateRequest.ExternalReference, preapproval.ExternalReference);
        }

        [Fact]
        public void Update_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = client.Create(request);

            Thread.Sleep(1000);

            PreApprovalUpdateRequest updateRequest = BuildUpdateRequest();
            PreApproval preapproval =
                client.Update(createdPreApproval.Id, updateRequest);

            Assert.NotNull(preapproval);
            Assert.Equal(updateRequest.ExternalReference, preapproval.ExternalReference);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = await client.CreateAsync(request);

            await Task.Delay(1000);

            PreApproval preapproval =
                await client.GetAsync(createdPreApproval.Id);

            Assert.NotNull(preapproval);
            Assert.Equal(createdPreApproval.Id, preapproval.Id);
        }

        [Fact]
        public void Get_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = client.Create(request);

            Thread.Sleep(1000);

            PreApproval preapproval = client.Get(createdPreApproval.Id);

            Assert.NotNull(preapproval);
            Assert.Equal(createdPreApproval.Id, preapproval.Id);
        }

        [Fact]
        public async Task SearchAsync_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = await client.CreateAsync(request);

            await Task.Delay(3000);

            var searchRequest = new SearchRequest
            {
                Limit = 1,
                Offset = 0,
                Filters = new Dictionary<string, object>
                {
                    ["id"] = createdPreApproval.Id,
                },
            };
            ResultsResourcesPage<PreApproval> results =
                await client.SearchAsync(searchRequest);

            Assert.NotNull(results);
            Assert.NotNull(results.Paging);
            Assert.Equal(1, results.Paging.Total);
            Assert.NotNull(results.Results);
            Assert.Equal(createdPreApproval.Id, results.Results.First().Id);
        }

        [Fact]
        public void Search_Success()
        {
            PreApprovalCreateRequest request = BuildCreateRequest();
            PreApproval createdPreApproval = client.Create(request);

            Thread.Sleep(3000);

            var searchRequest = new SearchRequest
            {
                Limit = 1,
                Offset = 0,
                Filters = new Dictionary<string, object>
                {
                    ["id"] = createdPreApproval.Id,
                },
            };
            ResultsResourcesPage<PreApproval> results =
                client.Search(searchRequest);

            Assert.NotNull(results);
            Assert.NotNull(results.Paging);
            Assert.Equal(1, results.Paging.Total);
            Assert.NotNull(results.Results);
            Assert.Equal(createdPreApproval.Id, results.Results.First().Id);
        }

        private PreApprovalCreateRequest BuildCreateRequest()
        {
            return new PreApprovalCreateRequest
            {
                CollectorId = User.Id,
                BackUrl = "https://backurl.com",
                ExternalReference = Guid.NewGuid().ToString(),
                PayerEmail = Environment.GetEnvironmentVariable("USER_EMAIL"),
                Reason = "New recurring",
                AutoRecurring = new PreApprovalAutoRecurringCreateRequest
                {
                    CurrencyId = CurrencyHelper.GetCurrencyId(User),
                    TransactionAmount = 100,
                    Frequency = 1,
                    FrequencyType = "months",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(3),
                },
            };
        }

        private PreApprovalUpdateRequest BuildUpdateRequest()
        {
            return new PreApprovalUpdateRequest
            {
                BackUrl = "https://new.backurl.com",
                ExternalReference = Guid.NewGuid().ToString(),
                Reason = "Updated recurring",
                AutoRecurring = new PreApprovalAutoRecurringUpdateRequest
                {
                    TransactionAmount = 50,
                    StartDate = DateTime.Now.AddMonths(6),
                    EndDate = DateTime.Now.AddYears(5),
                },
            };
        }
    }
}
