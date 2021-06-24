// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AccountDataClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void AccountDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var accountDataClientStub = new AccountDataClientStub();

            // Assert.
            Assert.NotNull(accountDataClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Add()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), AccountTemplate.User).ConfigureAwait(false);

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Change()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Connect()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Connect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Disconnect()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Disconnect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Get_By_Id()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Get_By_Name()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var account = await accountDataClientStub.Get(Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_GetAll()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            var accounts = await accountDataClientStub
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Empty(accounts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task AccountDataClientStub_Remove()
        {
            // Arrange.
            var accountDataClientStub = new AccountDataClientStub();

            // Act.
            await accountDataClientStub.Remove(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
        }
    }
}
