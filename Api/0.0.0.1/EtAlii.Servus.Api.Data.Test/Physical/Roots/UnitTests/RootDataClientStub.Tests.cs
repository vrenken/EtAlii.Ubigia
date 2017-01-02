namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class RootDataClientStub_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Create()
        {
            var rootDataClientStub = new RootDataClientStub();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Add()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Add(Guid.NewGuid().ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Change()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Connect()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Connect();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Disconnect()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Disconnect();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Get_By_Name()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.Get(Guid.NewGuid().ToString());
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Get_By_Guid()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_GetAll()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = rootDataClientStub.GetAll();
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void RootDataClientStub_Remove()
        {
            var rootDataClientStub = new RootDataClientStub();
            rootDataClientStub.Remove(Guid.NewGuid());
        }
    }
}
