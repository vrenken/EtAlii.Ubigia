namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;

    public class AddressFactoryTests : IDisposable
    {
        private const string BaseAddress = "http://localtesthost:1234";
        private IAddressFactory _factory;
        private Storage _storage;

        public AddressFactoryTests()
        {
            _factory = new AddressFactory();
            _storage = new Storage { Address = BaseAddress };
        }

        public void Dispose()
        {
            _factory = null;
            _storage = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AddressFactory_Create()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test");

            // Assert.
            Assert.Equal(BaseAddress + UriHelper.Delimiter + "test", address.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AddressFactory_Create_With_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "firstvalue", "secondKey", "secondvalue");

            // Assert.
            Assert.Equal(BaseAddress + UriHelper.Delimiter + "test?firstkey=firstvalue&secondKey=secondvalue", address.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AddressFactory_Create_With_Special_Parameters()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, "test", "firstkey", "first=value", "secondKey", "second&value");

            // Assert.
            Assert.Equal(BaseAddress + UriHelper.Delimiter + "test?firstkey=first%3Dvalue&secondKey=second%26value", address.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AddressFactory_Create_Storage_Is_Null()
        {
            // Arrange.

            // Act.
            var act = new Action(() => _factory.Create((Storage)null, "test"));

            // Assert.
            Assert.Throws<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AddressFactory_Create_Path_Is_Null()
        {
            // Arrange.

            // Act.
            var address = _factory.Create(_storage, null);

            // Assert.
            Assert.Equal(BaseAddress + UriHelper.Delimiter, address.ToString());
        }
    }
}
