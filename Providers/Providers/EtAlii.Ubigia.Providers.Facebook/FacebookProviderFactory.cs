namespace EtAlii.Ubigia.Provisioning.Facebook
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class FacebookProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemDataContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, FacebookProvider>();

            return container.GetInstance<IProvider>();
        }
    }
}
