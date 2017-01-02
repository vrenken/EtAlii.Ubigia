namespace EtAlii.Servus.Infrastructure.SignalR
{
    using EtAlii.Servus.Api.Transport;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using SimpleInjector;

    internal class SignalRApiScaffolding : IScaffolding
    {
        private readonly DefaultDependencyResolver _signalRDependencyResolver;

        public SignalRApiScaffolding(DefaultDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            container.Register<DefaultDependencyResolver>(() => _signalRDependencyResolver, Lifestyle.Singleton);
            container.Register<IParameterResolver,SignalRParameterResolver>(Lifestyle.Singleton);
            container.Register<ISerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);

            container.Register<ISignalUserRApiComponent, SignalUserRApiComponent>(Lifestyle.Transient);
            container.Register<ISignalAdminRApiComponent, SignalAdminRApiComponent>(Lifestyle.Transient);

            container.Register<ISignalRAuthenticationVerifier, SignalRAuthenticationVerifier>(Lifestyle.Singleton);
            container.Register<ISignalRAuthenticationTokenVerifier, SignalRAuthenticationTokenVerifier>(Lifestyle.Singleton);

            // We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
            _signalRDependencyResolver.Register(typeof(JsonSerializer), () => (JsonSerializer)container.GetInstance<ISerializer>());
            _signalRDependencyResolver.Register(typeof(IParameterResolver), container.GetInstance<IParameterResolver>);
        }
    }
}