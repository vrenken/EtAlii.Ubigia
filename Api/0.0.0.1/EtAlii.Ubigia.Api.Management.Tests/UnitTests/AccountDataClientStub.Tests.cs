namespace EtAlii.Ubigia.Api.Management.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Management.Tests;
    using EtAlii.Ubigia.Api.Transport;
    
    using Xunit;

    
    public class AccountDataClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void AccountDataClientStub_Create()
        {
            var accountDataClientStub = new AccountDataClientStub();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Add()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), AccountTemplate.User);

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Change()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Connect()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Disconnect()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Disconnect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Get_By_Id()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Get_By_Name()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Get(Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_GetAll()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var accounts = await accountDataClientStub.GetAll();

            // Assert.
            Assert.Null(accounts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Remove()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Remove(Guid.NewGuid());

            // Assert.
        }
    }
}
