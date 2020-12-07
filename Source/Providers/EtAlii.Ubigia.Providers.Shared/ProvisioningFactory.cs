namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningFactory : IProvisioningFactory
    {
        public IProvisioningManager Create(IProvisioningConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ProvisioningScaffolding2(configuration),
                new ProvisioningScaffolding(configuration.ProviderConfigurations, connection => configuration.CreateScriptContext(connection)),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.GetExtensions<IProvisioningExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IProvisioningManager>();
        }
    }
}
