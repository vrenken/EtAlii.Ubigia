namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class TransportUnitTestContext : IAsyncLifetime
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public ByteArrayComparer ByteArrayComparer { get; }
        public ContentComparer ContentComparer { get; }
        public PropertyDictionaryComparer PropertyDictionaryComparer { get; }

        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestContentFactory TestContentFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        public TestIdentifierFactory TestIdentifierFactory { get; }
        public TransportUnitTestContext()
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
            TransportTestContext = new TransportTestContext().Create();
            await TransportTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await TransportTestContext.Stop();
            TransportTestContext = null;
        }
    }
}