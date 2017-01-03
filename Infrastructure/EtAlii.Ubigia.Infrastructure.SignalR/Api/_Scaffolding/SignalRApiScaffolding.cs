namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRApiScaffolding(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            container.Register<IDependencyResolver>(() => _signalRDependencyResolver);
            container.Register<IParameterResolver,SignalRParameterResolver>();
            container.Register<ISerializer>(() => new SerializerFactory().Create());

            container.Register<ISignalRAuthenticationVerifier, SignalRAuthenticationVerifier>();
            container.Register<ISignalRAuthenticationTokenVerifier, SignalRAuthenticationTokenVerifier>();

            // We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
            _signalRDependencyResolver.Register(typeof(JsonSerializer), () => (JsonSerializer)container.GetInstance<ISerializer>());
            _signalRDependencyResolver.Register(typeof(IParameterResolver), container.GetInstance<IParameterResolver>);
        }
    }
}