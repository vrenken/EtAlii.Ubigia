// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    public abstract class StorageUnitTestContextBase : IDisposable
    {
        public IConfiguration HostConfiguration { get; }

        public TestContentFactory TestContentFactory { get; }
        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        protected StorageUnitTestContextBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            // var details = await new ConfigurationDetailsParser()
            //     .ParseForTesting(_hostConfigurationFile, portRange)
            //     .ConfigureAwait(false);
            // Folders = details.Folders;
            // Hosts = details.Hosts;
            // Ports = details.Ports;
            // Paths = details.Paths;
            //
            // var hostConfigurationRoot = new ConfigurationBuilder()
            //     .AddConfigurationDetails(details)
            //     .AddConfiguration(DiagnosticsConfiguration.Instance) // For testing we'll override the configured logging et.
            //     .Build();
            // HostConfiguration = hostConfigurationRoot;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                // Nothing to do here.
            }
        }

        ~StorageUnitTestContextBase()
        {
            Dispose(false);
        }
    }
}
