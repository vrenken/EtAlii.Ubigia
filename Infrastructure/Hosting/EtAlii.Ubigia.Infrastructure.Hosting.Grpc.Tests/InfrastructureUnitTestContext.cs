namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public class InfrastructureUnitTestContext : IDisposable
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
            HostTestContext.Start();
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
                HostTestContext.Stop();
                HostTestContext = null;
            }
        }

        ~InfrastructureUnitTestContext()
        {
            Dispose(false);
        }
    }
}