namespace EtAlii.Servus.Provisioning.Hosting
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningScaffolding : IScaffolding
    {
        private readonly IProviderConfiguration[] _providerConfigurations;
        private readonly Func<IDataConnection, IDataContext> _dataContextFactory;

        public ProvisioningScaffolding(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IDataContext> dataContextFactory)
        {
            _providerConfigurations = providerConfigurations;
            _dataContextFactory = dataContextFactory;
        }

        public void Register(Container container)
        {
            container.Register<IProviderManager, ProviderManager>();
            container.Register<IProvidersContext, ProvidersContext>();
            container.RegisterInitializer<IProvidersContext>(context => context.Initialize(_providerConfigurations, _dataContextFactory));
            container.Register<ISerializer>(() => new SerializerFactory().Create());
        }
    }
}