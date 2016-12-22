namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Provisioning;

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
