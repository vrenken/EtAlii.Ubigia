namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningScaffolding : IScaffolding
    {
        private readonly ProviderConfiguration[] _providerConfigurations;
        private readonly Func<IDataConnection, IGraphSLScriptContext> _scriptContextFactory;

        public ProvisioningScaffolding(ProviderConfiguration[] providerConfigurations, Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory)
        {
            _providerConfigurations = providerConfigurations;
            _scriptContextFactory = scriptContextFactory;
        }

        public void Register(Container container)
        {
            container.Register<IProviderManager, ProviderManager>();
            container.Register<IProvidersContext, ProvidersContext>();
            container.RegisterInitializer<IProvidersContext>(context => context.Initialize(_providerConfigurations, _scriptContextFactory));
            container.Register(() => new SerializerFactory().Create());
        }
    }
}