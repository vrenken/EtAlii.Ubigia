namespace EtAlii.Servus.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Logging;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;

    public class ComponentManager : IComponentManager
    {
        private readonly HttpConfiguration _httpConfiguration;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private HttpListener _httpListener;
        private readonly ILogger _logger;

        public ComponentManager(
            ILogger logger, 
            HttpConfiguration httpConfiguration, IDiagnosticsConfiguration diagnostics)
        {
            _logger = logger;
            _httpConfiguration = httpConfiguration;
            _diagnostics = diagnostics;
        }

        public void Start(IAppBuilder application)
        {
            var httpListenerName = typeof (HttpListener).FullName;
            if (application.Properties.ContainsKey(httpListenerName))
            {
                _httpListener = (HttpListener) application.Properties[httpListenerName];
            }

            //_httpConfiguration.Routes.Clear();

            StartNotifications(application);
            StartServices(application);
        }

        private void StartServices(IAppBuilder application)
        {
            _logger.Info("Starting WebAPI services");

            _httpConfiguration.MapHttpAttributeRoutes();

            _httpConfiguration.Routes.MapHttpRoute
            (
                name: "AuthenticationRoute",
                routeTemplate: "{controller}"
            );

            _httpConfiguration.Routes.MapHttpRoute
            (
                name: "ManagementRoute",
                routeTemplate: "management/{controller}"
            );

            //GlobalFilters.Filters.Add(new HandleErrorAttribute());
            //GlobalFilters.Filters.Add(new HttpsAttribute());
            //GlobalFilters.Filters.Add(new AuthenticationFilter());
            //GlobalFilters.Filters.Add(new RequiresAuthenticationTokenAttribute());

            _httpConfiguration.Formatters.Add(new BsonMediaTypeFormatter());

            if (_diagnostics.EnableLogging)
            {
                _httpConfiguration.EnableSystemDiagnosticsTracing();
            }

            application.UseWebApi(_httpConfiguration);

            _httpConfiguration.EnsureInitialized();

            _logger.Info("Started WebAPI services");
        }

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

        public void Stop()
        {
            // TODO: Why do we dispose this instance? Can the start be called again???
            _httpConfiguration.Dispose();

            if (_httpListener != null)
            {
                try
                {
                    _httpListener.Abort();
                    _httpListener = null;
                }
                catch
                {
                    Debugger.Break();
                }
            }
        }
    }
}