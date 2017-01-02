namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class ProviderHostFactory<TProviderHost> : IProviderHostFactory
        where TProviderHost : class, IProviderHost
    {
        public ProviderHostFactory()
        {
        }

        public IProviderHost Create(IHostConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new HostScaffolding<TProviderHost>(configuration), 
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

            return container.GetInstance<IProviderHost>();
        }
    }
}
