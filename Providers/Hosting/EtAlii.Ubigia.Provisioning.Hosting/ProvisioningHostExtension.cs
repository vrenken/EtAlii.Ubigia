namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using xTechnology.Hosting;

    public class ProvisioningHostExtension : IHostExtension
    {
        private readonly IProvisioning _provisioning;

        public ProvisioningHostExtension(IProvisioning provisioning)
        {
            _provisioning = provisioning;
        }

        public void Register(Container container)
        {
            container.Register(() => _provisioning);
            container.Register<IProvisioningService, ProvisioningService>();
        }
    }
}
