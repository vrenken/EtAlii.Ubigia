namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using xTechnology.Hosting;

    public class ProvisioningHostExtension : IHostExtension
    {
        private readonly IProvisioningManager _provisioningManager;

        public ProvisioningHostExtension(IProvisioningManager provisioningManager)
        {
            _provisioningManager = provisioningManager;
        }

        public void Register(Container container)
        {
            container.Register(() => _provisioningManager);
            container.Register<IProvisioningService, ProvisioningService>();
        }
    }
}
