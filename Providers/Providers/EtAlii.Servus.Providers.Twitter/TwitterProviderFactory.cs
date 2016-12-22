namespace EtAlii.Servus.Provisioning.Twitter
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.xTechnology.Logging;

    public class TwitterProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register<IProviderConfiguration>(() => configuration);
            container.Register<IDataContext>(() => configuration.SystemDataContext);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, TwitterProvider>();
            container.Register<ITweetImporter, TweetImporter>();

            container.Register<ILogger>(() => configuration.LogFactory.Create("Twitter", "Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
