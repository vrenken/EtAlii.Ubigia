namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class StorageTest : IAsyncLifetime
    {
        private PowerShellTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new PowerShellTestContext();
            await _testContext.Start().ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext.Stop().ConfigureAwait(false);
            _testContext = null;
        }

        [Fact]
        public void PowerShell_Storage_Select()
        {
            // Arrange.
            var expectedStorageAddress = new Uri($"{_testContext.Context.ServiceDetails.ManagementAddress.Scheme}://{_testContext.Context.ServiceDetails.ManagementAddress.Host}/").ToString();

            // Act.
            var result = _testContext.InvokeSelectStorage();

            // Assert.

            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            var storage = result[0].BaseObject as Storage;
            Assert.NotNull(storage);
	        Assert.Equal(expectedStorageAddress, storage.Address);
        }

        [Fact]
        public void PowerShell_Storages_Get()
        {
            // Arrange.
            _testContext.InvokeSelectStorage();

            // Act.
            var result = _testContext.InvokeGetStorages();
            var storages = _testContext.ToAssertedResults<Storage>(result);
            var firstCount = storages.Length;

            _testContext.InvokeAddStorage();

            result = _testContext.InvokeGetStorages();
            storages = _testContext.ToAssertedResults<Storage>(result);
            
            // Assert.
            Assert.True(storages.Length == firstCount + 1);
        }


        [Fact]
        public void PowerShell_Storage_Update()
        {
            // Arrange.
            _testContext.InvokeSelectStorage();
            _testContext.InvokeGetStorages();

            var firstName = Guid.NewGuid().ToString();
            var firstAddress = Guid.NewGuid().ToString();
            _testContext.InvokeAddStorage(firstName, firstAddress);

            var result = _testContext.InvokeGetStorageByName(firstName);
            var storage = _testContext.ToAssertedResult<Storage>(result);

            Assert.Equal(storage.Name, firstName);
            Assert.Equal(storage.Address, firstAddress);

            var secondName = Guid.NewGuid().ToString();
            var secondAddress = Guid.NewGuid().ToString();

            storage.Name = secondName;
            storage.Address = secondAddress;

            // Act.
            _testContext.InvokeUpdateStorage(storage);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetStorageByName(firstName);
                _testContext.ToAssertedResult<Storage>(result);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            
            //Assert.
            Assert.NotNull(exceptedException);

            result = _testContext.InvokeGetStorageByName(secondName);
            storage = _testContext.ToAssertedResult<Storage>(result);

            Assert.Equal(storage.Name, secondName);
            Assert.Equal(storage.Address, secondAddress);
        }


        [Fact]
        public void PowerShell_Storage_Remove_By_Id()
        {
            _testContext.InvokeSelectStorage();

            var storageName = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            _testContext.InvokeAddStorage(storageName, address);

            var result = _testContext.InvokeGetStorageByName(storageName);
            var storage = _testContext.ToAssertedResult<Storage>(result);

            Assert.Equal(storage.Name, storageName);

            _testContext.InvokeRemoveStorageById(storage.Id);

            result = _testContext.InvokeGetStorageByName(storageName);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.Null(result[0]);
        }

        [Fact]
        public void PowerShell_Storage_Remove_By_Name()
        {
            _testContext.InvokeSelectStorage();

            var storageName = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            _testContext.InvokeAddStorage(storageName, address);

            var result = _testContext.InvokeGetStorageByName(storageName);
            var storage = _testContext.ToAssertedResult<Storage>(result);

            Assert.Equal(storage.Name, storageName);

            _testContext.InvokeRemoveStorageByName(storage.Name);

            result = _testContext.InvokeGetStorageByName(storageName);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.Null(result[0]);
        }

        [Fact]
        public void PowerShell_Storage_Remove_By_Instance()
        {
            _testContext.InvokeSelectStorage();

            var storageName = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            _testContext.InvokeAddStorage(storageName, address);

            var result = _testContext.InvokeGetStorageByName(storageName);
            var storage = _testContext.ToAssertedResult<Storage>(result);
            Assert.NotNull(result);

            Assert.Equal(storage.Name, storageName);

            _testContext.InvokeRemoveStorageByInstance(storage);

            result = _testContext.InvokeGetStorageByName(storageName);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.Null(result[0]);
        }
    }
}
