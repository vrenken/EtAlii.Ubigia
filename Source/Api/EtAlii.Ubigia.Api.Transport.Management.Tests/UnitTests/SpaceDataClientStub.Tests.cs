// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class SpaceDataClientStubTests
    {
        [Fact]
        public void SpaceDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Assert.
            Assert.NotNull(spaceDataClientStub);
        }

        [Fact]
        public async Task SpaceDataClientStub_Add()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Add(Guid.NewGuid(), Guid.NewGuid().ToString(), SpaceTemplate.Data).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceDataClientStub_Change()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceDataClientStub_Connect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            await spaceDataClientStub.Connect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task SpaceDataClientStub_Disconnect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            await spaceDataClientStub.Disconnect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task SpaceDataClientStub_Get()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceDataClientStub_Get_By_Account()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Get(Guid.NewGuid(), Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceDataClientStub_GetAll()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var spaces = await spaceDataClientStub
                .GetAll(Guid.NewGuid())
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Empty(spaces);
        }
    }
}
