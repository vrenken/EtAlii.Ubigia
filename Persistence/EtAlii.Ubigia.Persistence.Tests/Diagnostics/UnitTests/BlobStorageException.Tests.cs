namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using Xunit;

    public class BlobStorageExceptionTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobStorageException_Create_1()
        {
            // Arrange.
            var innerException = new Exception();
            // Act.
            var exception = new BlobStorageException("Test message", innerException);

            // Assert.
            Assert.Equal("Test message", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobStorageException_Create_2()
        {
            // Arrange.

            // Act.
            var exception = new BlobStorageException("Test message");

            // Assert.
            Assert.Equal("Test message", exception.Message);
        }
    }
}