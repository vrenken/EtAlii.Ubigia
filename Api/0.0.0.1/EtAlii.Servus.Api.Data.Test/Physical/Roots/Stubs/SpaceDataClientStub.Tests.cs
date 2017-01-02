namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Management;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class SpaceDataClientStub_Tests
    {
        [TestMethod]
        public void SpaceDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var spaceDataClientStub = new SpaceClientStub();

            // Assert.
        }

        [TestMethod]
        public void SpaceDataClientStub_Add()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            var space = spaceDataClientStub.Add(Guid.NewGuid(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(space);
        }

        [TestMethod]
        public void SpaceDataClientStub_Change()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            var space = spaceDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(space);
        }

        [TestMethod]
        public void SpaceDataClientStub_Connect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            spaceDataClientStub.Connect();

            // Assert.
        }

        [TestMethod]
        public void SpaceDataClientStub_Disconnect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            spaceDataClientStub.Disconnect();

            // Assert.
        }

        [TestMethod]
        public void SpaceDataClientStub_Get()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            var space = spaceDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.IsNull(space);
        }

        [TestMethod]
        public void SpaceDataClientStub_Get_By_Account()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            var space = spaceDataClientStub.Get(Guid.NewGuid(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(space);
        }

        [TestMethod]
        public void SpaceDataClientStub_GetAll()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceClientStub();

            // Act.
            var spaces = spaceDataClientStub.GetAll(Guid.NewGuid());

            // Assert.
            Assert.IsNull(spaces);
        }
    }
}
