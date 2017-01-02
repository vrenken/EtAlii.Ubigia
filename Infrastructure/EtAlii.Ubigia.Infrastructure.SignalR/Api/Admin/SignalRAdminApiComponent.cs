namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;


    public partial class SignalRAdminApiComponent : ISignalRAdminApiComponent
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRAdminApiComponent(
            IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Start(IAppBuilder application)
        {
            //_logger.Info("Starting SignalR services");

            var signalRConfiguration = new HubConfiguration
            {
                Resolver = _signalRDependencyResolver,
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = false,
                EnableJSONP = true, // Something with x domain referencing: http://en.wikipedia.org/wiki/JSONP
            };

            application.Map(RelativeUri.AdminData, map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(signalRConfiguration);
            });
            //application.MapSignalR(signalRConfiguration);
            //Debug(webApiConfiguration);

            //_logger.Info("Started SignalR services");
        }

        public void Stop()
        {
            //throw new System.NotImplementedException();
        }
    }
}