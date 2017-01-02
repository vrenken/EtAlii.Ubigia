namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentNotificationClientStub_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentNotificationClientStub_Create()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentNotificationClientStub_Connect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Connect();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentNotificationClientStub_Disconnect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Disconnect();
        }
    }
}
