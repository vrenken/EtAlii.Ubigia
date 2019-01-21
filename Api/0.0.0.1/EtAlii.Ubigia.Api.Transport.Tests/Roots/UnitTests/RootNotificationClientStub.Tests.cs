namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class RootNotificationClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Create()
        {
            // Arrange.
            
            // Act.
            var rootNotificationClientStub = new RootNotificationClientStub();
            
            // Assert.
            Assert.NotNull(rootNotificationClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootNotificationClientStub_Connect()
        {
            // Arrange.
            var rootNotificationClientStub = new RootNotificationClientStub();

            // Act.
            await rootNotificationClientStub.Connect(null);
            
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootNotificationClientStub_Disconnect()
        {
            // Arrange.
            var rootNotificationClientStub = new RootNotificationClientStub();

            // Act.
            await rootNotificationClientStub.Disconnect(null);
            
            // Assert.
        }
    }
}
