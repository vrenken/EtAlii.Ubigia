namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddressFactory_Tests
    {
        private const string BaseAddress = "http://localtesthost:1234";
        private IAddressFactory _factory;
        private Storage _storage;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new AddressFactory();
            _storage = new Storage { Address = BaseAddress };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _factory = null;
            _storage = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void AddressFactory_Create()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test");

            // Assert.
            Assert.AreEqual(address, BaseAddress + "/test");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void AddressFactory_Create_With_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "firstvalue", "secondKey", "secondvalue");

            // Assert.
            Assert.AreEqual(address, BaseAddress + "/test?firstkey=firstvalue&secondKey=secondvalue");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void AddressFactory_Create_With_Special_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "first=value", "secondKey", "second&value");

            // Assert.
            Assert.AreEqual(address, BaseAddress + "/test?firstkey=first%3Dvalue&secondKey=second%26value");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void AddressFactory_Create_Storage_Is_Null()
        {
            // Arrange.

            // Act.
            var act = new Action(() => _factory.Create(null, "test"));

            // Assert.
            ExceptionAssert.Throws<NullReferenceException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void AddressFactory_Create_Path_Is_Null()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, null);

            // Assert.
            Assert.AreEqual(address, BaseAddress + "/");
        }
    }
}
