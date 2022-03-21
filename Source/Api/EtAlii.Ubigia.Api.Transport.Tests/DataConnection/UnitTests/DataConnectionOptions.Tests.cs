// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    [CorrelateUnitTests]
    public class DataConnectionOptionsTests
    {
        [Fact]
        public void DataConnectionOptions_Create()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();

            // Act.
            var options = new DataConnectionOptions(configurationRoot);

            // Assert.
            Assert.NotNull(options);
        }

        [Fact]
        public void DataConnectionOptions_UseTransport()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);
            var transportProvider = new StubbedTransportProvider();

            // Act.
            options.UseTransport(transportProvider);


            // Assert.
            Assert.NotNull(options.TransportProvider);
        }


        [Fact]
        public void DataConnectionOptions_UseStubbedConnection()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);

            // Act.
            options.UseStubbedConnection();

            // Assert.
            Assert.NotNull(options.TransportProvider);
        }


        [Fact]
        public void DataConnectionOptions_UseFactoryExtension()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);
            IDataConnection FactoryExtension() => null;

            // Act.
            options.Use(FactoryExtension);

            // Assert.
            Assert.Equal(FactoryExtension, options.FactoryExtension);
        }

        [Fact]
        public void DataConnectionOptions_UseCredentials()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);

            // Act.
            options.Use("AccountName", "Space", "Password");


            // Assert.
            Assert.Equal("AccountName", options.AccountName);
            Assert.Equal("Space", options.Space);
            Assert.Equal("Password", options.Password);
        }

        [Fact]
        public void DataConnectionOptions_UseCredentials_NoAccount()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);

            // Act.
            var act = new Action(() => options.Use(null, "Space", "Password"));

            // Assert.
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void DataConnectionOptions_UseCredentials_NoSpace()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);

            // Act.
            var act = new Action(() => options.Use("AccountName", null, "Password"));

            // Assert.
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void DataConnectionOptions_UseUri()
        {
            // Arrange.
            var settings = new Dictionary<string, string>
            {
                {"Service1:url", "http://somewhere"},
                {"Service1:port", "123"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(settings);
            var configurationRoot = configurationBuilder.Build();
            var options = new DataConnectionOptions(configurationRoot);
            var uri = new Uri("https://SomewhereExiting.com");
            // Act.
            options.Use(uri);

            // Assert.
            Assert.Equal(uri, options.Address);
        }
    }
}
