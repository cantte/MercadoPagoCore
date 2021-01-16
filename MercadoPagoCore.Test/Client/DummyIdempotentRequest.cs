namespace MercadoPagoCore.Tests.Client
{
    using MercadoPagoCore.Client;

    /// <summary>
    /// Dummy request for tests.
    /// </summary>
    public class DummyIdempotentRequest : IdempotentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyRequest"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        public DummyIdempotentRequest(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyRequest"/> class.
        /// </summary>
        public DummyIdempotentRequest()
        {
        }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
    }
}
