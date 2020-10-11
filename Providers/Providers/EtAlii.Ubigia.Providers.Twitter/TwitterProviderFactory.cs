namespace EtAlii.Ubigia.Provisioning.Twitter
{
    public class TwitterProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemScriptContext);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, TwitterProvider>();
            container.Register<ITweetImporter, TweetImporter>();
            
            return container.GetInstance<IProvider>();
        }
    }
}
