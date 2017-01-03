namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.WebApi.Portal.User;
    using EtAlii.xTechnology.MicroContainer;
    using Owin;

    public partial class WebApiComponentManagerFactory
    {
        public IWebApiComponentManager Create(Container container)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new MicroContainerDependencyResolver(container)
            };

            var userPortalComponent = new UserPortalComponent();
            var adminPortalComponent = new AdminPortalComponent();
            
            var webApiComponentManager = new WebApiComponentManager(httpConfiguration, userPortalComponent, adminPortalComponent);
            return webApiComponentManager;
        }
    }
}