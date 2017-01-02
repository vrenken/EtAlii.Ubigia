namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Provisioning;

    public class WindowsServiceHost : ProviderHostBase
    {
        public WindowsServiceHost(
            IDataContext data,
            IHostConfiguration configuration, 
            IProviderManager providerManager)
            : base(data, configuration, providerManager)
        {
        }
    }
}
