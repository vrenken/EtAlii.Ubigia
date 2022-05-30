// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Xunit;

    public sealed class KestrelConfiguratorTests
    {
        [Fact]
        public async Task KestrelConfigurator_Configure()
        {
            // Arrange.
            var details = await new ConfigurationDetailsParser()
                .ParseForTesting(ConfigurationFiles.HostSettingsSystems2VariantGrpc, UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);

            var configurationRoot = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
                .Build();

            // Act.
            var host = Host
                .CreateDefaultBuilder()
                .UseHostLogging(configurationRoot, typeof(KestrelConfiguratorTests).Assembly)
                .UseHostServices<LocalHostServicesFactory>(configurationRoot)
                .Build();

            // Assert.
            Assert.NotNull(host);

            // Assure.
            await host.StopAsync().ConfigureAwait(false);
        }
    }
}
