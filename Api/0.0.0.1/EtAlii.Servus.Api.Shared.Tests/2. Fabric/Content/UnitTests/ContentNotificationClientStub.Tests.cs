namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Api.Transport;
    using Xunit;

    
    public class ContentNotificationClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Create()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Connect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Connect(null);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Disconnect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Disconnect(null);
        }
    }
}
