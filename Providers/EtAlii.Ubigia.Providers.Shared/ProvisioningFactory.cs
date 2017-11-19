namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningFactory : IProvisioningFactory
    {
        public IProvisioning Create(IProvisioningConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ProvisioningScaffolding2(configuration),
                new ProvisioningScaffolding(configuration.ProviderConfigurations, connection => configuration.CreateDataContext(connection)),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IProvisioning>();
        }
    }
}
