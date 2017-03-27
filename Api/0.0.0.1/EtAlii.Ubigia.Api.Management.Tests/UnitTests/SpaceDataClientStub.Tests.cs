namespace EtAlii.Ubigia.Api.Management.Tests.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class SpaceDataClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void SpaceDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Add()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Add(Guid.NewGuid(), Guid.NewGuid().ToString(), SpaceTemplate.Data);

            // Assert.
            Assert.Null(space);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Change()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(space);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Connect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            await spaceDataClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Disconnect()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            await spaceDataClientStub.Disconnect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Get()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.Null(space);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_Get_By_Account()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var space = await spaceDataClientStub.Get(Guid.NewGuid(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(space);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SpaceDataClientStub_GetAll()
        {
            // Arrange.
            var spaceDataClientStub = new SpaceDataClientStub();

            // Act.
            var spaces = await spaceDataClientStub.GetAll(Guid.NewGuid());

            // Assert.
            Assert.Null(spaces);
        }
    }
}
