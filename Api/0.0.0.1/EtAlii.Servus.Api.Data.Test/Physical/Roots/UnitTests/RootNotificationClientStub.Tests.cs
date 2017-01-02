namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RootNotificationClientStub_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootNotificationClientStub_Create()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootNotificationClientStub_Connect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Connect();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootNotificationClientStub_Disconnect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Disconnect();
        }
    }
}
