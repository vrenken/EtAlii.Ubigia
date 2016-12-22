namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Provisioning;

    public class InMemoryHost : ProviderHostBase
    {
        public InMemoryHost(
            IDataContext data,
            IHostConfiguration configuration,
            IProviderManager providerManager)
            : base(data, configuration, providerManager)
        {
        }
    }
}
