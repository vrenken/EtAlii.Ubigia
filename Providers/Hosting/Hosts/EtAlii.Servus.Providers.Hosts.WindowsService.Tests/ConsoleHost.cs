namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Api.Functional;

    public class ConsoleHost : ProviderHostBase
    {
        public ConsoleHost(
            IDataContext data,
            IHostConfiguration configuration, 
            IProviderManager providerManager) 
            : base(data, configuration, providerManager)
        {
        }
    }
}
