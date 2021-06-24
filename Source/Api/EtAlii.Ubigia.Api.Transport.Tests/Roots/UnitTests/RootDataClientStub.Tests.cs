// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class RootDataClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var rootDataClientStub = new RootDataClientStub();

            // Assert.
            Assert.NotNull(rootDataClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Add()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            await rootDataClientStub.Add(Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Change()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            await rootDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Connect()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            await rootDataClientStub.Connect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Disconnect()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            await rootDataClientStub.Disconnect().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Name()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            var result = await rootDataClientStub.Get(Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Guid()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            var result = await rootDataClientStub.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_GetAll()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            var result = await rootDataClientStub
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Remove()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();

            // Act.
            await rootDataClientStub.Remove(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
        }
    }
}
