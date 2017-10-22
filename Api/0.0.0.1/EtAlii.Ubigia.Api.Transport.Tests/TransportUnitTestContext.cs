namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Tests;

    public class TransportUnitTestContext : IDisposable
    {
        public ITransportTestContext TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public ByteArrayComparer ByteArrayComparer { get; }
        public ContentComparer ContentComparer { get; }

        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestContentFactory TestContentFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        public TransportUnitTestContext()
        {
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestContentFactory = new TestContentFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            ByteArrayComparer = new ByteArrayComparer();
            ContentComparer = new ContentComparer(ByteArrayComparer);

            var task = Task.Run(async () =>
            {
                TransportTestContext = new TransportTestContext().Create();
                await TransportTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await TransportTestContext.Stop();
                TransportTestContext = null;
            });
            task.Wait();
        }
    }
}