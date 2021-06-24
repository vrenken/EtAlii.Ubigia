// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Provisioning
{
    using EtAlii.xTechnology.Hosting.Tests.Provisioning.Firewall;
    using EtAlii.xTechnology.MicroContainer;

    public class ProvisioningSystemCommandsFactory : ISystemCommandsFactory
    {
        public ICommand[] Create(ISystem system)
        {
            var container = new Container();

            container.Register(() => system);

            container.Register<IProcessStarter, ProcessStarter>();
            container.Register<IWebsiteBrowser, WebsiteBrowser>();

            container.Register<IOpenProvisioningPortalCommand, OpenProvisioningPortalCommand>();

            container.Register<IStartSystemCommand>(() => new StartSystemCommand(system, "Provisioning/Start"));
            container.Register<IStopSystemCommand>(() => new StopSystemCommand(system, "Provisioning/Stop"));
            container.Register<IConfigureFirewallRulesCommand>(() => new ConfigureProvisioningFirewallRulesCommand(system));
            
            return new ICommand[]
            {
                container.GetInstance<IStartSystemCommand>(),
                container.GetInstance<IStopSystemCommand>(),
                container.GetInstance<IConfigureFirewallRulesCommand>(),

                container.GetInstance<IOpenProvisioningPortalCommand>(),

            };
        }
    }
}