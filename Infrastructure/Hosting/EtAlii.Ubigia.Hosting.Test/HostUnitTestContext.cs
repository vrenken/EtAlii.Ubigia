namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;

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