namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using System.IO;
    using Xunit;

    public class AuthenticationTokenSerializationTests
    {
        [Fact]
        public void AuthenticationToken_Serialize_Deserialize()
        {
            // Arrange.
            var token = new AuthenticationToken
            {
                Address = "http://nowhere.com",
                Salt = 12345,
                Name = "Test name"
            };

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);

            // Act.
            writer.Write(token);
            stream.Position = 0;
            var token2 = reader.Read<AuthenticationToken>();

            // Assert.
            Assert.NotNull(token2);
            Assert.Equal(token.Address, token2.Address);
            Assert.Equal(token.Salt, token2.Salt);
            Assert.Equal(token.Name, token2.Name);
        }
    }
}
