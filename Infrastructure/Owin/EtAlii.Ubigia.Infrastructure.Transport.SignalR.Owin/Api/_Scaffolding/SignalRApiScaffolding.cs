namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
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
            container.Register(() => _signalRDependencyResolver);
            container.Register<IParameterResolver,SignalRParameterResolver>();
            container.Register(() => new SerializerFactory().Create());

	        container.Register<ISimpleAuthenticationVerifier, SimpleAuthenticationVerifier>();
			container.Register<ISimpleAuthenticationBuilder, SimpleAuthenticationBuilder>();
            container.Register<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>();

            // We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
            _signalRDependencyResolver.Register(typeof(JsonSerializer), () => (JsonSerializer)container.GetInstance<ISerializer>());
            _signalRDependencyResolver.Register(typeof(IParameterResolver), container.GetInstance<IParameterResolver>);
        }
    }
}