using System.Threading;
using System.Threading.Tasks;
using MercadoPagoCore.Error;
using MercadoPagoCore.Http;
using MercadoPagoCore.Resource;
using MercadoPagoCore.Serialization;

namespace MercadoPagoCore.Client.PreApproval
{
    /// <summary>
    /// Client that use the PreApproval APIs.
    /// </summary>
    public class PreApprovalClient : MercadoPagoClient<Resource.PreApproval.PreApproval>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreApprovalClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public PreApprovalClient(IHttpClient httpClient, ISerializer serializer) : base(httpClient, serializer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreApprovalClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        public PreApprovalClient(IHttpClient httpClient) : this(httpClient, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreApprovalClient"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public PreApprovalClient(ISerializer serializer) : this(null, serializer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreApprovalClient"/> class.
        /// </summary>
        public PreApprovalClient() : this(null, null)
        {
        }

        /// <summary>
        /// Get async a PreApproval by your ID.
        /// </summary>
        /// <param name="id">The PreApproval ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.PreApproval.PreApproval> GetAsync(
            string id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync($"/preapproval/{id}", HttpMethod.Get, null, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Get a PreApproval by your ID.
        /// </summary>
        /// <param name="id">The PreApproval ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.PreApproval.PreApproval Get(
            string id,
            RequestOptions requestOptions = null)
        {
            return Send($"/preapproval/{id}", HttpMethod.Get, null, requestOptions);
        }

        /// <summary>
        /// Creates a PreApproval as an asynchronous operation.
        /// </summary>
        /// <param name="request">The data to create a PreApproval.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the created PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/subscriptions/_preapproval/post/">here</a>.
        /// </remarks>
        public Task<Resource.PreApproval.PreApproval> CreateAsync(
            PreApprovalCreateRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync("/preapproval", HttpMethod.Post, request, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Creates a PreApproval.
        /// </summary>
        /// <param name="request">The data to create a PreApproval.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The created PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/subscriptions/_preapproval/post/">here</a>.
        /// </remarks>
        public Resource.PreApproval.PreApproval Create(
            PreApprovalCreateRequest request,
            RequestOptions requestOptions = null)
        {
            return Send("/preapproval", HttpMethod.Post, request, requestOptions);
        }

        /// <summary>
        /// Updates a PreApproval as an asynchronous operation.
        /// Just send in <paramref name="request"/> the properties you want to update.
        /// </summary>
        /// <param name="id">The PreApproval ID.</param>
        /// <param name="request">The data to update the PreApproval.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the updated PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.PreApproval.PreApproval> UpdateAsync(
            string id,
            PreApprovalUpdateRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync($"/preapproval/{id}", HttpMethod.Put, request, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Updates a PreApproval.
        /// Just send in <paramref name="request"/> the properties you want to update.
        /// </summary>
        /// <param name="id">The PreApproval ID.</param>
        /// <param name="request">The data to update the PreApproval.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The updated PreApproval.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.PreApproval.PreApproval Update(
            string id,
            PreApprovalUpdateRequest request,
            RequestOptions requestOptions = null)
        {
            return Send($"/preapproval/{id}", HttpMethod.Put, request, requestOptions);
        }

        /// <summary>
        /// Searches async for Preapprovals that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is a page of Preapprovals.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<ResultsResourcesPage<Resource.PreApproval.PreApproval>> SearchAsync(
            SearchRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SearchAsync<ResultsResourcesPage<Resource.PreApproval.PreApproval>>(
                "/preapproval/search",
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Searches for Preapprovals that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A page of Preapprovals.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public ResultsResourcesPage<Resource.PreApproval.PreApproval> Search(
            SearchRequest request,
            RequestOptions requestOptions = null)
        {
            return Search<ResultsResourcesPage<Resource.PreApproval.PreApproval>>(
                "/preapproval/search",
                request,
                requestOptions);
        }
    }
}
