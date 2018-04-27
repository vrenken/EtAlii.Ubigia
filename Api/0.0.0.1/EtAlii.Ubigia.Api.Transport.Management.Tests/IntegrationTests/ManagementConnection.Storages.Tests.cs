namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using Xunit;

    
    public class ManagementConnectionStoragesTests : IDisposable
    {
        private static ITransportTestContext<InProcessInfrastructureHostTestContext> _testContext;

        public ManagementConnectionStoragesTests()
        {
            var task = Task.Run(async () =>
            {
                _testContext = new TransportTestContext().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Add()
        {
            var connection = await _testContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = await connection.Storages.Add(name, address);

            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Add_Multiple()
        {
            var connection = await _testContext.CreateManagementConnection();

            for(int i=0; i<10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = await connection.Storages.Add(name, address);
            
                Assert.NotNull(storage);
                Assert.Equal(name, storage.Name);
                Assert.Equal(address, storage.Address);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get()
        {
            var connection = await _testContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = await connection.Storages.Add(name, address);

            storage = await connection.Storages.Get(storage.Id);

            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get_Multiple()
        {
            var connection = await _testContext.CreateManagementConnection();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = await connection.Storages.Add(name, address);

                storage = await connection.Storages.Get(storage.Id);

                Assert.NotNull(storage);
                Assert.Equal(name, storage.Name);
                Assert.Equal(address, storage.Address);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get_First_Full_Add()
        {
            var connection = await _testContext.CreateManagementConnection();

            var storages = new List<Storage>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = await connection.Storages.Add(name, address);
                Assert.NotNull(storage);
                Assert.Equal(name, storage.Name);
                Assert.Equal(address, storage.Address);
                storages.Add(storage);
            }

            foreach (var storage in storages)
            {
                var retrievedStorage = await connection.Storages.Get(storage.Id);

                Assert.NotNull(retrievedStorage);
                Assert.Equal(storage.Name, retrievedStorage.Name);
                Assert.Equal(storage.Address, retrievedStorage.Address);
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get_None()
        {
            var connection = await _testContext.CreateManagementConnection();
            var retrievedStorages = await connection.Storages.GetAll();
            var retrievedStorage = retrievedStorages.SingleOrDefault();
            Assert.NotNull(retrievedStorage);
            Assert.Equal(_testContext.Context.HostAddress, new Uri(retrievedStorage.Address, UriKind.Absolute));
            Assert.Equal(_testContext.Context.HostName, retrievedStorage.Name);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void ManagementConnection_Storages_Get_All()
        //{
        //    var connection = CreateManagementConnection();

        //    var storages = new List<ItemStorage>(); 

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var name = Guid.NewGuid().ToString();
        //        var address = Guid.NewGuid().ToString();

        //        var storage = await connection.Storages.Add(name, address);
        //        Assert.NotNull(storage);
        //        Assert.Equal(name, storage.Name);
        //        Assert.Equal(address, storage.Address);
        //        storages.Add(storage);
        //    }

        //    var retrievedStorages = await connection.Storages.GetAll();

        //    Assert.Equal(storages.Count + 1, retrievedStorages.Count());

        //    foreach (var storage in storages)
        //    {
        //        var matchingStorage = retrievedStorages.Single(s => s.Id == storage.Id);
        //        Assert.NotNull(matchingStorage);
        //        Assert.Equal(storage.Name, matchingStorage.Name);
        //        Assert.Equal(storage.Address, matchingStorage.Address);
        //    }
        //}

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Change()
        {
            var connection = await _testContext.CreateManagementConnection();
	        var index = 10;
            var name = Guid.NewGuid().ToString();
            var address = $"http://www.host{++index}.com";

			var storage = await connection.Storages.Add(name, address);
            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);

            storage = await connection.Storages.Get(storage.Id);
            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);

            name = Guid.NewGuid().ToString();
	        address = $"http://www.host{++index}.com";
            storage = await connection.Storages.Change(storage.Id, name, address);
            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);

            storage = await connection.Storages.Get(storage.Id);
            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Delete()
        {
            var connection = await _testContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = await connection.Storages.Add(name, address);
            Assert.NotNull(storage);
            Assert.Equal(name, storage.Name);
            Assert.Equal(address, storage.Address);

            storage = await connection.Storages.Get(storage.Id);
            Assert.NotNull(storage);

            await connection.Storages.Remove(storage.Id);

            storage = await connection.Storages.Get(storage.Id);
            Assert.Null(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Delete_Non_Existing()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection();
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await connection.Storages.Remove(id));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Change_Non_Existing()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await connection.Storages.Change(id, name, address));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Add_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Storages.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            // Assert.
            Assert.Null(connection.Storages);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Storages.Get(Guid.NewGuid()));

            // Assert.
            Assert.Null(connection.Storages);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Remove_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Storages.Remove(Guid.NewGuid()));

            // Assert.
            Assert.Null(connection.Storages);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Get_All_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Storages.GetAll());

            // Assert.
            Assert.Null(connection.Storages);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Change_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Storages.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            // Assert.
            Assert.Null(connection.Storages);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Storages_Add_Already_Existing()
        {
            // Arrange.
            var connection = await _testContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();
            var storage = await connection.Storages.Add(name, address);
            Assert.NotNull(storage);

            // Act.
            var act = new Func<Task>(async () => await connection.Storages.Add(name, address));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
    }
}
