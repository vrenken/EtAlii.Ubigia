namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionScaffolding : IScaffolding
    {
        private readonly ISpaceConnectionConfiguration _configuration;

        public SpaceConnectionScaffolding(ISpaceConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register(() => _configuration.Transport);

            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertiesContext, PropertiesContext>();
        }
    }
}
