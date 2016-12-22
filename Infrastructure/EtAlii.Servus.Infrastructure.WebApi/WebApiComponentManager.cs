namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.User;
    using Owin;

    public partial class WebApiComponentManager : IWebApiComponentManager
    {
        private readonly HttpConfiguration _httpConfiguration;
        private readonly IComponent[] _components;

        private HttpListener _httpListener;

        public WebApiComponentManager(
            HttpConfiguration httpConfiguration, 
            IUserPortalComponent userPortalComponent,
            IWebApiUserApiComponent webApiUserApiComponent,
            IAdminPortalComponent adminPortalComponent, 
            IWebApiAdminApiComponent webApiAdminApiComponent)
        {
            _httpConfiguration = httpConfiguration;

            _components = new IComponent[]
            {
                userPortalComponent,
                webApiUserApiComponent,

                adminPortalComponent,
                webApiAdminApiComponent,
            };
        }

        public void Start(IAppBuilder application)
        {
            var httpListenerName = typeof (HttpListener).FullName;
            if (application.Properties.ContainsKey(httpListenerName))
            {
                _httpListener = (HttpListener) application.Properties[httpListenerName];
            }

            foreach (var component in _components)
            {
                component.Start(application);
            }
        }

        public void Stop()
        {
            foreach (var component in _components.Reverse())
            {
                component.Stop();
            }

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