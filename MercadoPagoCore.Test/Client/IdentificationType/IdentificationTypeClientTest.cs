namespace MercadoPagoCore.Tests.Client.IdentificationType
{
    using System.Threading.Tasks;
    using MercadoPagoCore.Client.IdentificationType;
    using MercadoPagoCore.Resource;
    using MercadoPagoCore.Resource.IdentificationType;
    using Xunit;

    public class IdentificationTypeClientTest : BaseClientTest
    {
        private readonly IdentificationTypeClient identificationTypeClient;

        public IdentificationTypeClientTest(ClientFixture clientFixture)
            : base(clientFixture)
        {
            identificationTypeClient = new IdentificationTypeClient();
        }

        [Fact]
        public async Task ListPaymentMethodsAsync_Success()
        {
            ResourcesList<IdentificationType> identificationTypes =
                await identificationTypeClient.ListAsync();

            Assert.NotNull(identificationTypes);
            Assert.True(identificationTypes.Count > 0);
        }

        [Fact]
        public void ListPaymentMethods_Success()
        {
            ResourcesList<IdentificationType> identificationTypes =
                identificationTypeClient.List();

            Assert.NotNull(identificationTypes);
            Assert.True(identificationTypes.Count > 0);
        }
    }
}
