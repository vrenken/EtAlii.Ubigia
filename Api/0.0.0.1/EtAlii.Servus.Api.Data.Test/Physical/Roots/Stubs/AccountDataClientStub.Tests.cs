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
    public class AccountDataClientStub_Tests
    {
        [TestMethod]
        public void AccountDataClientStub_Create()
        {
            var accountDataClientStub = new AccountClientStub();
        }

        [TestMethod]
        public void AccountDataClientStub_Add()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            var account = accountDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AccountDataClientStub_Change()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            var account = accountDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AccountDataClientStub_Connect()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            accountDataClientStub.Connect();

            // Assert.
        }

        [TestMethod]
        public void AccountDataClientStub_Disconnect()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            accountDataClientStub.Disconnect();

            // Assert.
        }

        [TestMethod]
        public void AccountDataClientStub_Get_By_Id()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            var account = accountDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AccountDataClientStub_Get_By_Name()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            var account = accountDataClientStub.Get(Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AccountDataClientStub_GetAll()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            var accounts = accountDataClientStub.GetAll();

            // Assert.
            Assert.IsNull(accounts);
        }

        [TestMethod]
        public void AccountDataClientStub_Remove()
        {
            // Arrange.
            var accountDataClientStub = new AccountClientStub();

            // Act.
            accountDataClientStub.Remove(Guid.NewGuid());

            // Assert.
        }
    }
}
