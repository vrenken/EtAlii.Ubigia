namespace EtAlii.Servus.Api.Tests
{
    using System;
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;

    public class TestHostingStorage : ITestHostingStorage
    {
        public string SpaceName { get; set; }
        
        public TestHostingStorage()
        {
        }

        public void Start(ITransportTestContext transportTestContext, ITestHosting testHosting)
        {
            SpaceName = Guid.NewGuid().ToString();
            transportTestContext.AddTestAccountAndSpace(testHosting.AccountName, testHosting.AccountPassword, SpaceName);
        }

        public void Stop()
        {
            SpaceName = null;
        }
    }
}
