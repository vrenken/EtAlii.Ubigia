// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class InfrastructureUnitTestContext : IAsyncLifetime
    {
        public IConfiguration HostConfiguration => Host.HostConfiguration;
        public IConfiguration ClientConfiguration => Host.ClientConfiguration;
        public InProcessInfrastructureHostTestContext Host { get; private set; }
        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestContentFactory TestContentFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }
        public ContentComparer ContentComparer { get; }
        public ByteArrayComparer ByteArrayComparer { get; }
        public PropertyDictionaryComparer PropertyDictionaryComparer { get; }

        public InfrastructureUnitTestContext()
        {
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestContentFactory = new TestContentFactory();
            TestPropertiesFactory = new TestPropertiesFactory();
            ByteArrayComparer = new ByteArrayComparer();
            ContentComparer = new ContentComparer(ByteArrayComparer);
            PropertyDictionaryComparer = new PropertyDictionaryComparer();

            Host = new HostTestContextFactory().Create<InProcessInfrastructureHostTestContext>();
        }

        public async Task InitializeAsync()
        {
            await Host.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Host.Stop().ConfigureAwait(false);
            Host = null;
        }
    }
}
