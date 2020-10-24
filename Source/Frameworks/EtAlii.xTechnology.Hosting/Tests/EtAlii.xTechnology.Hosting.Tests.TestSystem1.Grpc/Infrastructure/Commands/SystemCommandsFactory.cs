﻿namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
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
