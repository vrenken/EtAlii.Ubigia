﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using Newtonsoft.Json;
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;

    
    public class AuthenticationTokenConverter_Tests
    {
        [Fact]
        public void AuthenticationTokenConverter_Converter_Token()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var authenticationTokenConverter = new AuthenticationTokenConverter(serializer);

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
            var serializer = new SerializerFactory().Create();
            var authenticationTokenConverter = new AuthenticationTokenConverter(serializer);

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.ToBytes(null);
            });

            // Assert.
            ExceptionAssert.Throws<JsonWriterException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Null()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var authenticationTokenConverter = new AuthenticationTokenConverter(serializer);

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.FromBytes(null);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Empty()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var authenticationTokenConverter = new AuthenticationTokenConverter(serializer);

            // Act.
            var token = authenticationTokenConverter.FromBytes(new byte[] { });

            // Assert.
            Assert.Null(token);
        }

        [Fact]
        public void AuthenticationTokenConverter_FromBytes_Random()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var authenticationTokenConverter = new AuthenticationTokenConverter(serializer);

            // Act.
            var act = new Action(() =>
            {
                authenticationTokenConverter.FromBytes(new byte[] { 23, 55, 33, 254, 64 });
            });

            // Assert.
            ExceptionAssert.Throws<JsonReaderException>(act);
        }
    }
}
