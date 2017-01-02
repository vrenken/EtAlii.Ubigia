namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class ContentNotificationClientStub_Tests
    {
        [TestMethod]
        public void ContentNotificationClientStub_Create()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
        }

        [TestMethod]
        public void ContentNotificationClientStub_Connect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Connect();
        }

        [TestMethod]
        public void ContentNotificationClientStub_Disconnect()
        {
            var contentNotificationClientStub = new ContentNotificationClientStub();
            contentNotificationClientStub.Disconnect();
        }
    }
}
