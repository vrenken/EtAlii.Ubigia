namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using SimpleInjector;

    public class SignalRApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRApiScaffolding(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            container.Register<ISignalRComponentManager, SignalRComponentManager>(Lifestyle.Transient);

            container.Register<IDependencyResolver>(() => _signalRDependencyResolver, Lifestyle.Singleton);
            container.Register<IParameterResolver,SignalRParameterResolver>(Lifestyle.Singleton);
            container.Register<ISerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);

            container.Register<ISignalRUserApiComponent, SignalRUserApiComponent>(Lifestyle.Transient);
            container.Register<ISignalRAdminApiComponent, SignalRAdminApiComponent>(Lifestyle.Transient);

            container.Register<ISignalRAuthenticationVerifier, SignalRAuthenticationVerifier>(Lifestyle.Singleton);
            container.Register<ISignalRAuthenticationTokenVerifier, SignalRAuthenticationTokenVerifier>(Lifestyle.Singleton);

            // We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
            _signalRDependencyResolver.Register(typeof(JsonSerializer), () => (JsonSerializer)container.GetInstance<ISerializer>());
            _signalRDependencyResolver.Register(typeof(IParameterResolver), container.GetInstance<IParameterResolver>);
        }
    }
}