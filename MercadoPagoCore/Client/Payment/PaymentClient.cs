using System.Threading;
using System.Threading.Tasks;
using MercadoPagoCore.Error;
using MercadoPagoCore.Http;
using MercadoPagoCore.Resource;
using MercadoPagoCore.Resource.Payment;
using MercadoPagoCore.Serialization;

namespace MercadoPagoCore.Client.Payment
{
    /// <summary>
    /// Client that use the Payments APIs.
    /// </summary>
    public class PaymentClient : MercadoPagoClient<Resource.Payment.Payment>
    {
        private readonly PaymentRefundClient refundClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public PaymentClient(IHttpClient httpClient, ISerializer serializer)
            : base(httpClient, serializer)
        {
            refundClient = new PaymentRefundClient(httpClient, serializer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client that will be used in HTTP requests.</param>
        public PaymentClient(IHttpClient httpClient)
            : this(httpClient, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentClient"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The serializer that will be used to serialize the HTTP requests content
        /// and to deserialize the HTTP response content.
        /// </param>
        public PaymentClient(ISerializer serializer)
            : this(null, serializer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentClient"/> class.
        /// </summary>
        public PaymentClient()
            : this(null, null)
        {
        }

        /// <summary>
        /// Get async a payment by your ID.
        /// </summary>
        /// <param name="id">The payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments_id/get/">here</a>.
        /// </remarks>
        public Task<Resource.Payment.Payment> GetAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                $"/v1/payments/{id}",
                HttpMethod.Get,
                null,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Get a payment by your ID.
        /// </summary>
        /// <param name="id">The payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments_id/get/">here</a>.
        /// </remarks>
        public Resource.Payment.Payment Get(
            long id,
            RequestOptions requestOptions = null)
        {
            return Send(
                $"/v1/payments/{id}",
                HttpMethod.Get,
                null,
                requestOptions);
        }

        /// <summary>
        /// Creates a payment as an asynchronous operation.
        /// </summary>
        /// <param name="request">The data to create the payment.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the created payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments/post/">here</a>.
        /// </remarks>
        public Task<Resource.Payment.Payment> CreateAsync(
            PaymentCreateRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                "/v1/payments",
                HttpMethod.Post,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Creates a payment.
        /// </summary>
        /// <param name="request">The data to create the payment.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The created payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments/post/">here</a>.
        /// </remarks>
        public Resource.Payment.Payment Create(
            PaymentCreateRequest request,
            RequestOptions requestOptions = null)
        {
            return Send(
                "/v1/payments",
                HttpMethod.Post,
                request,
                requestOptions);
        }

        /// <summary>
        /// Cancels a pending payment async.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the cancelled payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.Payment.Payment> CancelAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            var request = new PaymentCancelRequest();
            return SendAsync(
                $"/v1/payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Cancels a pending payment.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The cancelled payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.Payment.Payment Cancel(
            long id,
            RequestOptions requestOptions = null)
        {
            var request = new PaymentCancelRequest();
            return Send(
                $"/v1/payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions);
        }

        /// <summary>
        /// Captures a authorized payment async.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the captured payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.Payment.Payment> CaptureAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return CaptureAsync(id, null, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Captures a pending payment.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The captured payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.Payment.Payment Capture(
            long id,
            RequestOptions requestOptions = null)
        {
            return Capture(id, null, requestOptions);
        }

        /// <summary>
        /// Captures a authorized payment async.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="amount">Amount to capture (if null, will capture the total payment amount).</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is the captured payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<Resource.Payment.Payment> CaptureAsync(
            long id,
            decimal? amount,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            var request = new PaymentCaptureRequest
            {
                TransactionAmount = amount,
            };
            return SendAsync(
                $"/v1/payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Captures a pending payment.
        /// </summary>
        /// <param name="id">Payment id.</param>
        /// <param name="amount">Amount to capture (if null, will capture the total payment amount).</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>The captured payment.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Resource.Payment.Payment Capture(
            long id,
            decimal? amount,
            RequestOptions requestOptions = null)
        {
            var request = new PaymentCaptureRequest
            {
                TransactionAmount = amount,
            };
            return Send(
                $"/v1/payments/{id}",
                HttpMethod.Put,
                request,
                requestOptions);
        }

        /// <summary>
        /// Searches async for payments that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task whose the result is a page of payments.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments_search/get/">here</a>.
        /// </remarks>
        public Task<ResultsResourcesPage<Resource.Payment.Payment>> SearchAsync(
            SearchRequest request,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return SearchAsync<ResultsResourcesPage<Resource.Payment.Payment>>(
                "/v1/payments/search",
                request,
                requestOptions,
                cancellationToken);
        }

        /// <summary>
        /// Searches for payments that match the criteria of <see cref="AdvancedSearchRequest"/>.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/>.</param>
        /// <returns>A page of payments.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        /// <remarks>
        /// Check the API documentation
        /// <a href="https://www.mercadopago.com/developers/en/reference/payments/_payments_search/get/">here</a>.
        /// </remarks>
        public ResultsResourcesPage<Resource.Payment.Payment> Search(
            SearchRequest request,
            RequestOptions requestOptions = null)
        {
            return Search<ResultsResourcesPage<Resource.Payment.Payment>>(
                "/v1/payments/search",
                request,
                requestOptions);
        }

        /// <summary>
        /// Creates async a refund for payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="amount">The amount to refund.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<PaymentRefund> RefundAsync(
            long id,
            decimal? amount,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.RefundAsync(id, amount, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Creates a refund for payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="amount">The amount to refund.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public PaymentRefund Refund(
            long id,
            decimal? amount,
            RequestOptions requestOptions = null)
        {
            return refundClient.Refund(id, amount, requestOptions);
        }

        /// <summary>
        /// Creates async a total refund for payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<PaymentRefund> RefundAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.RefundAsync(id, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Creates a total refund for payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>The refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public PaymentRefund Refund(
            long id,
            RequestOptions requestOptions = null)
        {
            return refundClient.Refund(id, requestOptions);
        }

        /// <summary>
        /// Gets async a refund by id from the payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="refundId">The refund ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<PaymentRefund> GetRefundAsync(
            long id,
            long refundId,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.GetAsync(id, refundId, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Gets a refund by id from the payment.
        /// </summary>
        /// <param name="paymentId">The payment ID.</param>
        /// <param name="refundId">The refund ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>A task whose the result is the refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public PaymentRefund GetRefund(
            long paymentId,
            long refundId,
            RequestOptions requestOptions = null)
        {
            return refundClient.Get(paymentId, refundId, requestOptions);
        }

        /// <summary>
        /// Lists async the refunds of the payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task whose the result is the list of refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public Task<ResourcesList<PaymentRefund>> ListRefundsAsync(
            long id,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            return refundClient.ListAsync(id, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Lists the refunds of the payment.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <param name="requestOptions"><see cref="RequestOptions"/></param>
        /// <returns>A task whose the result is the list of refund.</returns>
        /// <exception cref="MercadoPagoException">If a unexpected exception occurs.</exception>
        /// <exception cref="MercadoPagoApiException">If the API returns a error.</exception>
        public ResourcesList<PaymentRefund> ListRefunds(
            long id,
            RequestOptions requestOptions = null)
        {
            return refundClient.List(id, requestOptions);
        }
    }
}
