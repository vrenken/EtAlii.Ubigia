namespace EtAlii.Servus.Provisioning.Facebook
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;

    public class FacebookProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register<IProviderConfiguration>(() => configuration);
            container.Register<IDataContext>(() => configuration.SystemDataContext);
            container.Register<IManagementConnection>(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, FacebookProvider>();

            return container.GetInstance<IProvider>();
        }
    }
}
