namespace EtAlii.Ubigia.PowerShell.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Tests;
    using Xunit;

    
    public class Storage_Test : IDisposable
    {
        private PowerShellTestContext _testContext;

        public Storage_Test()
        {
            TestInitialize();
        }

        public void Dispose()
        {
            TestCleanup();
        }

        private void TestInitialize()
        {
            _testContext = new PowerShellTestContext();
            _testContext.Start();
        }

        private void TestCleanup()
        {
            _testContext.Stop();
            _testContext = null;

        }

        [Fact]
        public void PowerShell_Storage_Select()
        {
            // Arrange.

            // Act.
            var result = _testContext.InvokeSelectStorage();

            // Assert.

            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            var storage = result[0].BaseObject as Storage;
            Assert.NotNull(storage);
	        Assert.Equal(_testContext.Context.HostAddress.ToString(), storage.Address);//configuration.Address);
        }

        [Fact]
        public void PowerShell_Storages_Get()
        {
            _testContext.InvokeSelectStorage();

            var result = _testContext.InvokeGetStorages();
            var storages = _testContext.ToAssertedResult<List<Storage>>(result);
            var firstCount = storages.Count;

            _testContext.InvokeAddStorage();

            result = _testContext.InvokeGetStorages();
            storages = _testContext.ToAssertedResult<List<Storage>>(result);
            Assert.True(storages.Count == firstCount + 1);
        }


        [Fact]
        public void PowerShell_Storage_Update()
        {
            _testContext.InvokeSelectStorage();

            var result = _testContext.InvokeGetStorages();

            var firstName = Guid.NewGuid().ToString();
            var firstAddress = Guid.NewGuid().ToString();
            _testContext.InvokeAddStorage(firstName, firstAddress);

            result = _testContext.InvokeGetStorageByName(firstName);
            var storage = _testContext.ToAssertedResult<Storage>(result);

            Assert.Equal(storage.Name, firstName);
            Assert.Equal(storage.Address, firstAddress);

            var secondName = Guid.NewGuid().ToString();
            var secondAddress = Guid.NewGuid().ToString();

            storage.Name = secondName;
            storage.Address = secondAddress;

            _testContext.InvokeUpdateStorage(storage);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetStorageByName(firstName);
                storage = _testContext.ToAssertedResult<Storage>(result);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
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
