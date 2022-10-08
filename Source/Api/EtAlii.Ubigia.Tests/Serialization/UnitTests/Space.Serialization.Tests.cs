namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.IO;
    using Xunit;

    public class SpaceSerializationTests
    {
        [Fact]
        public void Space_Serialize_Deserialize()
        {
            // Arrange.
            var space = new Space
            {
                Id = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Name = "John Doe",
            };

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);

            // Act.
            writer.Write(space);
            stream.Position = 0;
            var space2 = reader.Read<Space>();

            // Assert.
            Assert.NotNull(space2);
            Assert.Equal(space.Id, space2.Id);
            Assert.Equal(space.AccountId, space2.AccountId);
            Assert.Equal(space.Name, space2.Name);
        }
    }
}
