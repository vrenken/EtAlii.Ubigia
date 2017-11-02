using System.Web.Configuration;
using EtAlii.Ubigia.Storage;
using EtAlii.xTechnology.Hosting;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.xTechnology.Diagnostics;
    using global::Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder application)
        {
            var applicationManager = new WebsiteApplicationManager(application);

            var configuration = new HostConfigurationBuilder().Build(sectionName => WebConfigurationManager.GetWebApplicationSection(sectionName), applicationManager);

            var host = new HostFactory<WebsiteHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

        }
    }
}
