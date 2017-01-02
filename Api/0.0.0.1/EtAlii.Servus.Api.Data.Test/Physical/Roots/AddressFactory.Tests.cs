namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    public class AddressFactory_Tests
    {
        private const string _baseAddress = "http://localtesthost:1234";
        private AddressFactory _factory;
        private Storage _storage;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new AddressFactory();
            _storage = new Storage { Address = _baseAddress };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _factory = null;
            _storage = null;
        }

        [TestMethod]
        public void AddressFactory_Create()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test");

            // Assert.
            Assert.AreEqual(address, _baseAddress + "/test");
        }

        [TestMethod]
        public void AddressFactory_Create_With_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "firstvalue", "secondKey", "secondvalue");

            // Assert.
            Assert.AreEqual(address, _baseAddress + "/test?firstkey=firstvalue&secondKey=secondvalue");
        }

        [TestMethod]
        public void AddressFactory_Create_With_Special_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "first=value", "secondKey", "second&value");

            // Assert.
            Assert.AreEqual(address, _baseAddress + "/test?firstkey=first%3Dvalue&secondKey=second%26value");
        }

        [TestMethod]
        public void AddressFactory_Create_Storage_Is_Null()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                _factory.Create(null, "test");
            });

            // Assert.
            ExceptionAssert.Throws<NullReferenceException>(act);
        }

        [TestMethod]
        public void AddressFactory_Create_Path_Is_Null()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, null);

            // Assert.
            Assert.AreEqual(address, _baseAddress + "/");
        }
    }
}
