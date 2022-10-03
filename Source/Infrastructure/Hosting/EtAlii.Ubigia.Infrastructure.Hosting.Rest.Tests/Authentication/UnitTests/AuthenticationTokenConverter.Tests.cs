// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using Newtonsoft.Json;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    // TODO: Move all instances of this test class to single testproject
    [CorrelateUnitTests]
    public class AuthenticationTokenConverterTests
    {
        [Fact]
        public void AuthenticationTokenConverter_Converter_Token()
        {
            // Arrange.
            var authenticationTokenConverter = new AuthenticationTokenConverter();

            var originalToken = new AuthenticationToken
            {
                Name = "name",
                Address = "address",
                Salt = 1234,
            };

            // Act.
            var tokenAsBytes = authenticationTokenConverter.ToBytes(originalToken);
            var newToken = authenticationTokenConverter.FromBytes(tokenAsBytes);

            // Assert.
            Assert.Equal(originalToken.Name, newToken.Name);
            Assert.Equal(originalToken.Address, newToken.Address);
            Assert.Equal(originalToken.Salt, newToken.Salt);
        }

        [Fact]
        public void AuthenticationTokenConverter_ToBytes_Null()
        {
            // Arrange.
            var authenticationTokenConverter = new AuthenticationTokenConverter();

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.ToBytes(null);
            });

            // Assert.
            Assert.Throws<JsonWriterException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Null()
        {
            // Arrange.
            var authenticationTokenConverter = new AuthenticationTokenConverter();

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.FromBytes(null);
            });

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Empty()
        {
            // Arrange.
            var authenticationTokenConverter = new AuthenticationTokenConverter();

            // Act.
            var token = authenticationTokenConverter.FromBytes(Array.Empty<byte>());

            // Assert.
            Assert.Null(token);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Random()
        {
            // Arrange.
            var authenticationTokenConverter = new AuthenticationTokenConverter();

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.FromBytes(new byte[] { 23, 55, 33, 254, 64 });
            });

            // Assert.
            Assert.Throws<JsonReaderException>(act);
        }
    }
}
