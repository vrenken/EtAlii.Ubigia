namespace EtAlii.Ubigia.Provisioning.Tests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningTestContextFactory
    {
        public IProvisioningTestContext Create()
        {
            var container = new Container();

            container.Register<IProvisioningTestContext, ProvisioningTestContext>();
            container.Register<IHostTestContextFactory, HostTestContextFactory>();

            return container.GetInstance<IProvisioningTestContext>();
        }
    }
}
