namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class RootNotificationClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Create()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Connect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Connect(null);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Disconnect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Disconnect(null);
        }
    }
}
