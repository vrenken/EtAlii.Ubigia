// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using Microsoft.Extensions.Configuration;

    public class FabricUnitTestContext : IAsyncLifetime
    {
        public ITransportTestContext Transport { get; private set; }

        public IConfiguration ClientConfiguration => Transport.Host.ClientConfiguration;
        public IConfiguration HostConfiguration => Transport.Host.HostConfiguration;

        public ByteArrayComparer ByteArrayComparer { get; }
        public ContentComparer ContentComparer { get; }
        public PropertyDictionaryComparer PropertyDictionaryComparer { get; }

        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestContentFactory TestContentFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        public TestIdentifierFactory TestIdentifierFactory { get; }

        public FabricUnitTestContext()
        {
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestContentFactory = new TestContentFactory();
            TestPropertiesFactory = new TestPropertiesFactory();
            TestIdentifierFactory = new TestIdentifierFactory();

            ByteArrayComparer = new ByteArrayComparer();
            ContentComparer = new ContentComparer(ByteArrayComparer);
            PropertyDictionaryComparer = new PropertyDictionaryComparer();
        }

        public async Task InitializeAsync()
        {
            Transport = new TransportTestContext().Create();
            await Transport.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Transport.Stop().ConfigureAwait(false);
            Transport = null;
        }
    }
}
