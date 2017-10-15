namespace EtAlii.Servus.Infrastructure.SignalR
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.SignalR;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;


    public partial class SignalAdminRApiComponent : ISignalAdminRApiComponent
    {
        private readonly DefaultDependencyResolver _signalRDependencyResolver;

        public SignalAdminRApiComponent(
            DefaultDependencyResolver signalRDependencyResolver)
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