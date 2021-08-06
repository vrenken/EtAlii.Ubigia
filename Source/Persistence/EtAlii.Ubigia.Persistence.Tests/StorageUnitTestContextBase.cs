// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Xunit;

    public abstract class StorageUnitTestContextBase : IAsyncLifetime
    {
        protected IConfigurationRoot HostConfiguration { get; private set; }

        private readonly ILogger _logger = Log.ForContext<StorageUnitTestContextBase>();

        public TestContentFactory Content { get; }
        public TestContentDefinitionFactory ContentDefinitions { get; }
        public TestPropertiesFactory Properties { get; }

        protected StorageUnitTestContextBase()
        {
            Content = new TestContentFactory();
            ContentDefinitions = new TestContentDefinitionFactory();
            Properties = new TestPropertiesFactory();
        }

        public virtual async Task InitializeAsync()
        {
            _logger.Verbose("Initializing StorageUnitTestContext");

            var details = await new ConfigurationDetailsParser()
                .ParseForTesting("HostSettings.json", UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);

            // We should make use of this storage folder somehow.
            // var folders = details.Folders;

            var hostConfigurationRoot = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
                .Build();
            HostConfiguration = hostConfigurationRoot;
        }

        public virtual Task DisposeAsync()
        {
            _logger.Verbose("Disposing StorageUnitTestContext");

            HostConfiguration = null;
            return Task.CompletedTask;
        }
    }
}
