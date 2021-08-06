// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.Extensions.Configuration;

    using Xunit;

    public class ConfigurationBuilderTests
    {
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationBuilder_Build(string configurationFile)
        {
            // Arrange.
            var details = await new ConfigurationDetailsParser().Parse(configurationFile).ConfigureAwait(false);

            var configurationBuilder = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot); // For testing we'll override the configured logging et.

            // Act.
            var configuration = configurationBuilder.Build();

            // Assert.
            Assert.NotNull(configuration);
        }
    }
}
