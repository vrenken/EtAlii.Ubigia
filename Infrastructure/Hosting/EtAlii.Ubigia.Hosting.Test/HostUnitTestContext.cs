namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public class HostUnitTestContext : IDisposable
    {
        public IHostTestContext HostTestContext { get; private set; }

        public HostUnitTestContext()
        {
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