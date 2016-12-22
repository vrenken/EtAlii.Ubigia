namespace EtAlii.Servus.Infrastructure.SignalR
{
    using EtAlii.Servus.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;
    using SimpleInjector;

    public class SignalRUserApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRUserApiScaffolding(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            container.Register<AuthenticationHub>(Lifestyle.Singleton);
            container.Register<EntryHub>(Lifestyle.Singleton);
            container.Register<ContentHub>(Lifestyle.Singleton);
            container.Register<ContentDefinitionHub>(Lifestyle.Singleton);
            container.Register<PropertiesHub>(Lifestyle.Singleton);
            container.Register<RootHub>(Lifestyle.Singleton);

            _signalRDependencyResolver.Register(typeof(AuthenticationHub), container.GetInstance<AuthenticationHub>);
            _signalRDependencyResolver.Register(typeof(EntryHub), container.GetInstance<EntryHub>);
            _signalRDependencyResolver.Register(typeof(ContentHub), container.GetInstance<ContentHub>);
            _signalRDependencyResolver.Register(typeof(ContentDefinitionHub), container.GetInstance<ContentDefinitionHub>);
            _signalRDependencyResolver.Register(typeof(PropertiesHub), container.GetInstance<PropertiesHub>);
            _signalRDependencyResolver.Register(typeof(RootHub), container.GetInstance<RootHub>);
        }
    }
}