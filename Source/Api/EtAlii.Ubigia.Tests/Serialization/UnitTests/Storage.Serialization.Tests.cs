namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.IO;
    using Xunit;

    public class StorageSerializationTests
    {
        [Fact]
        public void Storage_Serialize_Deserialize()
        {
            // Arrange.
            var storage = new Storage
            {
                Id = Guid.NewGuid(),
                Address = "http://nowhere.com",
                Name = "John Doe",
            };

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);

            // Act.
            writer.Write(storage);
            stream.Position = 0;
            var storage2 = reader.Read<Storage>();

            // Assert.
            Assert.NotNull(storage2);
            Assert.Equal(storage.Id, storage2.Id);
            Assert.Equal(storage.Address, storage2.Address);
            Assert.Equal(storage.Name, storage2.Name);
        }
    }
}
