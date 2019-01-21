namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class ContentNotificationClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Create()
        {
            // Arrange.
            
            // Act,
            var contentNotificationClientStub = new ContentNotificationClientStub();
            
            // Assert.
            Assert.NotNull(contentNotificationClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentNotificationClientStub_Connect()
        {
            // Arrange.
            var contentNotificationClientStub = new ContentNotificationClientStub();

            // Act,
            await contentNotificationClientStub.Connect(null);
        
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentNotificationClientStub_Disconnect()
        {
            // Arrange.
            var contentNotificationClientStub = new ContentNotificationClientStub();

            // Act,
            await contentNotificationClientStub.Disconnect(null);
        
            // Assert.
        }
    }
}
