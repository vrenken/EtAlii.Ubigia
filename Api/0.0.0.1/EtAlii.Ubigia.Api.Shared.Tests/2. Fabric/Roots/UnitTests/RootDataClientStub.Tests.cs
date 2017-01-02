namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    
    public class RootDataClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDataClientStub_Create()
        {
            var rootDataClientStub = new RootDataClientStub();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Add()
        {
            var rootDataClientStub = new RootDataClientStub();
            await rootDataClientStub.Add(Guid.NewGuid().ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Change()
        {
            var rootDataClientStub = new RootDataClientStub();
            await rootDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Connect()
        {
            var rootDataClientStub = new RootDataClientStub();
            await rootDataClientStub.Connect(null);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Disconnect()
        {
            var rootDataClientStub = new RootDataClientStub();
            await rootDataClientStub.Disconnect(null);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Name()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = await rootDataClientStub.Get(Guid.NewGuid().ToString());
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Get_By_Guid()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = await rootDataClientStub.Get(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_GetAll()
        {
            var rootDataClientStub = new RootDataClientStub();
            var result = await rootDataClientStub.GetAll();
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootDataClientStub_Remove()
        {
            var rootDataClientStub = new RootDataClientStub();
            await rootDataClientStub.Remove(Guid.NewGuid());
        }
    }
}
