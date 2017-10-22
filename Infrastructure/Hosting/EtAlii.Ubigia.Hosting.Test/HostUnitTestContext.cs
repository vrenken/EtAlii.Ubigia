namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Tests;

    public class HostUnitTestContext : IDisposable
    {
        public IHostTestContext HostTestContext { get; private set; }
        public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        public TestContentFactory TestContentFactory { get; }
        public TestPropertiesFactory TestPropertiesFactory { get; }

        public HostUnitTestContext()
        {
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestContentFactory = new TestContentFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            HostTestContext = new HostTestContextFactory().Create();
            HostTestContext.Start();

        }

        public void Dispose()
        {
            HostTestContext.Stop();
            HostTestContext = null;
        }
    }
}