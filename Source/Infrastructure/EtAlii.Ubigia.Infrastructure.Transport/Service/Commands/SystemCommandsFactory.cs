// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemCommandsFactory : ISystemCommandsFactory
    {
        public ICommand[] Create(ISystem system)
        {
            var container = new Container();

            container.Register(() => system);

            container.Register<IProcessStarter, ProcessStarter>();
            container.Register<IWebsiteBrowser, WebsiteBrowser>();

            container.Register<IOpenAdminPortalCommand, OpenAdminPortalCommand>();
            container.Register<IOpenUserPortalCommand, OpenUserPortalCommand>();

            container.Register<IStartSystemCommand>(() => new StartSystemCommand(system, "Portal/Start"));
            container.Register<IStopSystemCommand>(() => new StopSystemCommand(system, "Portal/Stop"));
            container.Register<IConfigureFirewallRulesCommand, ConfigureFirewallRulesCommand>();
            
            return new ICommand[]
            {
                container.GetInstance<IStartSystemCommand>(),
                container.GetInstance<IStopSystemCommand>(),
                container.GetInstance<IConfigureFirewallRulesCommand>(),

                container.GetInstance<IOpenAdminPortalCommand>(),
                container.GetInstance<IOpenUserPortalCommand>(),
            };
        }
    }
}
