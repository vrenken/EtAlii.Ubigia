namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
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
            await rootDataClientStub.Add(Guid.NewGuid().ToString());
            
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Change()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            await rootDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());
            
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Connect()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            await rootDataClientStub.Connect(null);
            
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Disconnect()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            await rootDataClientStub.Disconnect(null);
            
            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Name()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            var result = await rootDataClientStub.Get(Guid.NewGuid().ToString());
            
            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Guid()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            var result = await rootDataClientStub.Get(Guid.NewGuid());
            
            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_GetAll()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            var result = await rootDataClientStub.GetAll();
            
            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Remove()
        {
            // Arrange.
            var rootDataClientStub = new RootDataClientStub();
            
            // Act.
            await rootDataClientStub.Remove(Guid.NewGuid());

            // Assert.
        }
    }
}
