using System;
using System.Threading;
using System.Threading.Tasks;
using MercadoPagoCore.Error;
using MercadoPagoCore.Http;
using MercadoPagoCore.Resource;
using MercadoPagoCore.Resource.AdvancedPayment;
using MercadoPagoCore.Serialization;

namespace MercadoPagoCore.Client.AdvancedPayment
{
    /// <summary>
    /// Client that use the Advanced Payments APIs.
    /// </summary>
    public class AdvancedPaymentClient : MercadoPagoClient<Resource.AdvancedPayment.AdvancedPayment>
    {
        private readonly AdvancedPaymentRefundClient refundClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPaymentClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public AdvancedPaymentClient(IHttpClient httpClient, ISerializer serializer) : base(httpClient, serializer)
        {
            refundClient = new AdvancedPaymentRefundClient(httpClient, serializer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPaymentClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        public AdvancedPaymentClient(IHttpClient httpClient) : this(httpClient, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPaymentClient"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public AdvancedPaymentClient(ISerializer serializer) : this(null, serializer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPaymentClient"/> class.
        /// </summary>
        public AdvancedPaymentClient() : this(null, null)
        {
        }

        /// <summary>
        /// Get async a advanced payment by your ID.
        /// </summary>
        /// <param name="id">The advanced payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments_id/get/">here</a>.
        /// </remarks>
        public Task<Resource.AdvancedPayment.AdvancedPayment> GetAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Get,
                null,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Get a advanced payment by your ID.
        /// </summary>
        /// <param name="id">The advanced payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments_id/get/">here</a>.
        /// </remarks>
        public Resource.AdvancedPayment.AdvancedPayment Get(
            long id,
            RequestOptions requestOptions = null)
        {
            return Send(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Get,
                null,
                requestOptions);
        }

        /// <summary>
        /// Creates a advanced payment as an asynchronous operation.
        /// </summary>
        /// <param name="request">The data to create the advanced payment.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the created advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments/post/">here</a>.
        /// </remarks>
        public Task<Resource.AdvancedPayment.AdvancedPayment> CreateAsync(
            AdvancedPaymentCreateRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                "/v1/advanced_payments",
                HttpMethod.Post,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Creates a advanced payment.
        /// </summary>
        /// <param name="request">The data to create the advanced payment.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The created advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments/post/">here</a>.
        /// </remarks>
        public Resource.AdvancedPayment.AdvancedPayment Create(
            AdvancedPaymentCreateRequest request,
            RequestOptions requestOptions = null)
        {
            return Send(
                "/v1/advanced_payments",
                HttpMethod.Post,
                request,
                requestOptions);
        }

        /// <summary>
        /// Cancels a pending advanced payment async.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the cancelled advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.AdvancedPayment.AdvancedPayment> CancelAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            AdvancedPaymentCancelRequest request = new AdvancedPaymentCancelRequest();
            return SendAsync(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Cancels a pending advanced payment.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The cancelled advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.AdvancedPayment.AdvancedPayment Cancel(long id, RequestOptions requestOptions = null)
        {
            AdvancedPaymentCancelRequest request = new AdvancedPaymentCancelRequest();
            return Send(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions);
        }

        /// <summary>
        /// Captures a authorized advanced payment async.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the captured advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.AdvancedPayment.AdvancedPayment> CaptureAsync(long id, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            AdvancedPaymentCaptureRequest request = new AdvancedPaymentCaptureRequest();
            return SendAsync(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Captures a pending advanced payment.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The captured advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.AdvancedPayment.AdvancedPayment Capture(
            long id,
            RequestOptions requestOptions = null)
        {
            AdvancedPaymentCaptureRequest request = new AdvancedPaymentCaptureRequest();
            return Send(
                $"/v1/advanced_payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions);
        }

        /// <summary>
        /// Updates the release date of all disbursements of the advanced payment async.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="releaseDate">The money release date.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the updated advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.AdvancedPayment.AdvancedPayment> UpdateReleaseDateAsync(
            long id,
            DateTime releaseDate,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            AdvancedPaymentUpdateReleaseDateRequest request = new AdvancedPaymentUpdateReleaseDateRequest
            {
                MoneyReleaseDate = releaseDate,
            };
            return SendAsync(
                $"/v1/advanced_payments/{id}/disburses",
                HttpMethod.Post,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Updates the release date of all disbursements of the advanced payment.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="releaseDate">The money release date.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A task whose the result is the updated advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.AdvancedPayment.AdvancedPayment UpdateReleaseDate(
            long id,
            DateTime releaseDate,
            RequestOptions requestOptions = null)
        {
            AdvancedPaymentUpdateReleaseDateRequest request = new AdvancedPaymentUpdateReleaseDateRequest
            {
                MoneyReleaseDate = releaseDate,
            };
            return Send(
                $"/v1/advanced_payments/{id}/disburses",
                HttpMethod.Post,
                request,
                requestOptions);
        }

        /// <summary>
        /// Updates the release date of all disbursements of the advanced payment async.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="releaseDate">The money release date.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the updated advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.AdvancedPayment.AdvancedPayment> UpdateReleaseDateAsync(
            long id,
            long disbursementId,
            DateTime releaseDate,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            AdvancedPaymentUpdateReleaseDateRequest request = new AdvancedPaymentUpdateReleaseDateRequest
            {
                MoneyReleaseDate = releaseDate,
            };
            return SendAsync(
                $"/v1/advanced_payments/{id}/disbursements/{disbursementId}/disburses",
                HttpMethod.Post,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Updates the release date of all disbursements of the advanced payment.
        /// </summary>
        /// <param name="id">Advanced payment id.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="releaseDate">The money release date.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A task whose the result is the updated advanced payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.AdvancedPayment.AdvancedPayment UpdateReleaseDate(
            long id,
            long disbursementId,
            DateTime releaseDate,
            RequestOptions requestOptions = null)
        {
            AdvancedPaymentUpdateReleaseDateRequest request = new AdvancedPaymentUpdateReleaseDateRequest
            {
                MoneyReleaseDate = releaseDate,
            };
            return Send(
                $"/v1/advanced_payments/{id}/disbursements/{disbursementId}/disburses",
                HttpMethod.Post,
                request,
                requestOptions);
        }

        /// <summary>
        /// Searches async for advanced payments that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is a page of advanced payments.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments_id_search/get/">here</a>.
        /// </remarks>
        public Task<ResultsResourcesPage<Resource.AdvancedPayment.AdvancedPayment>> SearchAsync(
            SearchRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SearchAsync<ResultsResourcesPage<Resource.AdvancedPayment.AdvancedPayment>>(
                "/v1/advanced_payments/search",
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Searches for advanced payments that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A task whose the result is a page of advanced payments.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/advanced_payments/_advanced_payments_id_search/get/">here</a>.
        /// </remarks>
        public ResultsResourcesPage<Resource.AdvancedPayment.AdvancedPayment> Search(
            SearchRequest request,
            RequestOptions requestOptions = null)
        {
            return Search<ResultsResourcesPage<Resource.AdvancedPayment.AdvancedPayment>>(
                "/v1/advanced_payments/search",
                request,
                requestOptions);
        }

        /// <summary>
        /// Refunds async all disbursements of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the refunds list.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<ResourcesList<AdvancedPaymentDisbursementRefund>> RefundAllAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.RefundAllAsync(id, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Refunds async all disbursements of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A task whose the result is the refunds list.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public ResourcesList<AdvancedPaymentDisbursementRefund> RefundAll(
            long id,
            RequestOptions requestOptions = null)
        {
            return refundClient.RefundAll(id, requestOptions);
        }

        /// <summary>
        /// Refunds async a disbursement of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="amount">Amount to be refunded.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the refund of the disbursement.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<AdvancedPaymentDisbursementRefund> RefundAsync(
            long id,
            long disbursementId,
            decimal? amount,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.RefundAsync(
                id, disbursementId, amount, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Refunds a disbursement of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="amount">Amount to be refunded.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The refund of the disbursement.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public AdvancedPaymentDisbursementRefund Refund(
            long id,
            long disbursementId,
            decimal? amount,
            RequestOptions requestOptions = null)
        {
            return refundClient.Refund(
                id, disbursementId, amount, requestOptions);
        }

        /// <summary>
        /// Refunds async a disbursement of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the refund of the disbursement.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<AdvancedPaymentDisbursementRefund> RefundAsync(
            long id,
            long disbursementId,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.RefundAsync(
                id, disbursementId, null, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Refunds a disbursement of a advanced payment.
        /// </summary>
        /// <param name="id">Advanced Payment ID.</param>
        /// <param name="disbursementId">Disbursement ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The refund of the disbursement.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public AdvancedPaymentDisbursementRefund Refund(
            long id,
            long disbursementId,
            RequestOptions requestOptions = null)
        {
            return refundClient.Refund(
                id, disbursementId, null, requestOptions);
        }
    }
}
