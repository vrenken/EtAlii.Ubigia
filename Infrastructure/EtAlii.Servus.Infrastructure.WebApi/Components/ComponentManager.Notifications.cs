namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api.Transport;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;

    public partial class ComponentManager : IComponentManager
    {
        private void StartNotifications(IAppBuilder application)
        {
            _logger.Info("Starting SignalR services");

            var signalRConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = false,
                EnableJSONP = true, // Something with x domain referencing: http://en.wikipedia.org/wiki/JSONP
            };

            application.Map(RelativeUri.Notifications, map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(signalRConfiguration);
            });
            //application.MapSignalR(signalRConfiguration);
            //Debug(webApiConfiguration);

            _logger.Info("Started SignalR services");
        }
    }
}