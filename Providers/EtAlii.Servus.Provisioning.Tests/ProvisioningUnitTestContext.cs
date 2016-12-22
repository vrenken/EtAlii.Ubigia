namespace EtAlii.Servus.Provisioning.Tests
{
    using System;
    using System.Threading.Tasks;

    public class ProvisioningUnitTestContext : IDisposable
    {
        public IProvisioningTestContext ProvisioningTestContext { get; private set; }

        public ProvisioningUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                ProvisioningTestContext = new ProvisioningTestContextFactory().Create();
                await ProvisioningTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await ProvisioningTestContext.Stop();
                ProvisioningTestContext = null;
            });
            task.Wait();
        }

    }
}