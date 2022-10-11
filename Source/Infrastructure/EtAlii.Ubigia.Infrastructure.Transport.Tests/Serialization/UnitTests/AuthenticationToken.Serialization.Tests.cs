namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using System;
    using System.IO;
    using System.Text;
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

        [Fact]
        public void AuthenticationTokenConverter_ToBytes()
        {
            // Arrange.
            var converter = new AuthenticationTokenConverter();
            var token = new AuthenticationToken
            {
                Address = "http://nowhere.com",
                Salt = 12345,
                Name = "Test name"
            };

            // Act.
            var bytes = converter.ToBytes(token);

            // Assert.
            Assert.NotNull(bytes);
            var result = Encoding.UTF8.GetString(bytes);
            Assert.Equal("90\0\0\0\0\0\0\tTest namehttp://nowhere.com", result);
        }

        [Fact]
        public void AuthenticationTokenConverter_ToBytes_NUll()
        {
            // Arrange.
            var converter = new AuthenticationTokenConverter();

            // Act.
            var act = new Action(() => converter.ToBytes(null));

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes()
        {
            // Arrange.
            var bytes = Encoding.UTF8.GetBytes("90\0\0\0\0\0\0\tTest namehttp://nowhere.com");
            var converter = new AuthenticationTokenConverter();

            // Act.
            var token = converter.FromBytes(bytes);

            // Assert.
            Assert.NotNull(token);
            Assert.Equal(12345, token.Salt);
            Assert.Equal("Test name", token.Name);
            Assert.Equal("http://nowhere.com", token.Address);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Empty_Bytes()
        {
            // Arrange.
            var converter = new AuthenticationTokenConverter();
            var bytes = Array.Empty<byte>();

            // Act.
            var result = converter.FromBytes(bytes);

            // Assert.
            Assert.Null(result);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Null()
        {
            // Arrange.
            var converter = new AuthenticationTokenConverter();

            // Act.
            var act = new Action(() => converter.FromBytes(null));

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_To_From_String()
        {
            // Arrange.
            var converter = new AuthenticationTokenConverter();
            var token = new AuthenticationToken
            {
                Address = "http://nowhere.com",
                Salt = 12345,
                Name = "Test name"
            };

            // Act.
            var s = converter.ToString(token);
            var result = converter.FromString(s);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(12345, result.Salt);
            Assert.Equal("Test name", result.Name);
            Assert.Equal("http://nowhere.com", result.Address);
        }
    }
}
