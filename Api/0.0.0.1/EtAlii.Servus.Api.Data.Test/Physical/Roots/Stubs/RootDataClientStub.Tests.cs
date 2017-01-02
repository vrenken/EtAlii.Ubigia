namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class RootDataClientStub_Tests
    {
        [TestMethod]
        public void RootDataClientStub_Create()
        {
            var rootDataClientStub = new RootDataClientStub();
        }

        [TestMethod]
        public void RootDataClientStub_Add()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Add(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void RootDataClientStub_Change()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void RootDataClientStub_Connect()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Connect();
        }

        [TestMethod]
        public void RootDataClientStub_Disconnect()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Disconnect();
        }

        [TestMethod]
        public void RootDataClientStub_Get_By_Name()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.Get(Guid.NewGuid().ToString());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RootDataClientStub_Get_By_Guid()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RootDataClientStub_GetAll()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.GetAll();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RootDataClientStub_Remove()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Remove(Guid.NewGuid());
        }
    }
}
