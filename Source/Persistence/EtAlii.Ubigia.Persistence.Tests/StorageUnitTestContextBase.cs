// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public abstract class StorageUnitTestContextBase : IAsyncLifetime
    {
        public IConfiguration HostConfiguration { get; private set; }

        public TestContentFactory TestContentFactory { get; }
        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        protected StorageUnitTestContextBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();
        }

        public virtual async Task InitializeAsync()
        {
            var details = await new ConfigurationDetailsParser()
                .ParseForTesting("HostSettings.json", UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);

            // We should make use of this storage folder somehow.
            // var folders = details.Folders;

            var hostConfigurationRoot = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsConfiguration.Instance) // For testing we'll override the configured logging et.
                .Build();
            HostConfiguration = hostConfigurationRoot;
        }

        public virtual Task DisposeAsync()
        {
            HostConfiguration = null;
            return Task.CompletedTask;
        }
    }
}
