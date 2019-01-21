namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ProvisioningUnitTestContext : IAsyncLifetime
    {
        public IProvisioningTestContext ProvisioningTestContext { get; private set; }

        public ProvisioningUnitTestContext()
        {
        }

        public async Task InitializeAsync()
        {
            ProvisioningTestContext = new ProvisioningTestContextFactory().Create();
            await ProvisioningTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await ProvisioningTestContext.Stop();
            ProvisioningTestContext = null;
        }
    }
}