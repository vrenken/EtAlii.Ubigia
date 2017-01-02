namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class RootNotificationClientStub_Tests
    {
        [TestMethod]
        public void RootNotificationClientStub_Create()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
        }

        [TestMethod]
        public void RootNotificationClientStub_Connect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Connect();
        }

        [TestMethod]
        public void RootNotificationClientStub_Disconnect()
        {
            var rootNotificationClientStub = new RootNotificationClientStub();
            rootNotificationClientStub.Disconnect();
        }
    }
}
