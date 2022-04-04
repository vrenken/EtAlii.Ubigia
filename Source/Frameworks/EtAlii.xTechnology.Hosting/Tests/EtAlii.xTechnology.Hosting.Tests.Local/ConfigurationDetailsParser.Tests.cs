// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ConfigurationDetailsParserTests
    {

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_Parse(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .Parse(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_ParseForTesting_Host(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .ParseForTesting(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationHost", (IReadOnlyDictionary<string, string>)details.Hosts);
            Assert.Equal("127.0.0.1", details.Hosts["AuthenticationHost"]);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_Parse_Host(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .Parse(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationHost", (IReadOnlyDictionary<string, string>)details.Hosts);
            Assert.Equal("127.0.0.1", details.Hosts["AuthenticationHost"]);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_ParseForTesting_Port(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .ParseForTesting(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationPort", (IReadOnlyDictionary<string, int>)details.Ports);
            Assert.Equal(5003, details.Ports["AuthenticationPort"]);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_Parse_Port(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .Parse(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationPort", (IReadOnlyDictionary<string, int>)details.Ports);
            Assert.Equal(5003, details.Ports["AuthenticationPort"]);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_Parse_Folder(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .Parse(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationFolder", (IReadOnlyDictionary<string, string>)details.Folders);
            Assert.Equal("%localappdata%\\EtAlii\\App", details.Folders["AuthenticationFolder"]);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationDetailsParser_ParseForTesting_Folder(string configurationFile)
        {
            // Arrange.

            // Act.
            var details = await new ConfigurationDetailsParser()
                .ParseForTesting(configurationFile)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(details);
            Assert.NotNull(details.Hosts);
            Assert.Contains("AuthenticationFolder", (IReadOnlyDictionary<string, string>)details.Folders);
            Assert.NotEqual("%localappdata%\\EtAlii\\App", details.Folders["AuthenticationFolder"]);
        }

    }
}
