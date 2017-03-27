namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR;

    public class SignalRUserApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRUserApiScaffolding(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            _signalRDependencyResolver.Register(typeof(AuthenticationHub), () => new AuthenticationHub(container.GetInstance<ISignalRAuthenticationVerifier>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>(), container.GetInstance<IStorageRepository>()));
            _signalRDependencyResolver.Register(typeof(EntryHub), () => new EntryHub(container.GetInstance<IEntryRepository>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>()));
            _signalRDependencyResolver.Register(typeof(ContentHub), () => new ContentHub(container.GetInstance<IContentRepository>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>()));
            _signalRDependencyResolver.Register(typeof(ContentDefinitionHub), () => new ContentDefinitionHub(container.GetInstance<IContentDefinitionRepository>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>()));
            _signalRDependencyResolver.Register(typeof(PropertiesHub), () => new PropertiesHub(container.GetInstance<IPropertiesRepository>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>()));
            _signalRDependencyResolver.Register(typeof(RootHub), () => new RootHub(container.GetInstance<IRootRepository>(), container.GetInstance<ISignalRAuthenticationTokenVerifier>()));

            _signalRDependencyResolver.Register(typeof(IEntryRepository), container.GetInstance<IEntryRepository>);
            _signalRDependencyResolver.Register(typeof(IContentRepository), container.GetInstance<IContentRepository>);
            _signalRDependencyResolver.Register(typeof(IContentDefinitionRepository), container.GetInstance<IContentDefinitionRepository>);
            _signalRDependencyResolver.Register(typeof(IPropertiesRepository), container.GetInstance<IPropertiesRepository>);
            _signalRDependencyResolver.Register(typeof(IRootRepository), container.GetInstance<IRootRepository>);

            // IStorageRepository (already registered)
            _signalRDependencyResolver.Register(typeof(ISignalRAuthenticationVerifier), container.GetInstance<ISignalRAuthenticationVerifier>);
            _signalRDependencyResolver.Register(typeof(ISignalRAuthenticationTokenVerifier), container.GetInstance< ISignalRAuthenticationTokenVerifier>);
        }
    }
}