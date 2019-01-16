namespace EtAlii.Ubigia.Provisioning.Facebook
{
    public class FacebookProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemScriptContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, FacebookProvider>();

            return container.GetInstance<IProvider>();
        }
    }
}
