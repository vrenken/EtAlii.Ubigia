﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class InfrastructureUnitTestContext : IAsyncLifetime
    {
        public InProcessInfrastructureHostTestContext HostTestContext { get; private set; }
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

            HostTestContext = new HostTestContextFactory().Create<InProcessInfrastructureHostTestContext>();
        }

        public async Task InitializeAsync()
        {
            await HostTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await HostTestContext.Stop();
            HostTestContext = null;
        }
    }
}