namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.Ubigia.Api.Functional;

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
