// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Firewall
{
    public class ConfigureInfrastructureFirewallRulesCommand : ConfigureFirewallRulesCommandBase
    {
        public override string Name => "Admin/Configure firewall rules";

        public override string ScriptResourceName => "Infrastructure.Admin.Firewall.ConfigureFirewall.ps1";

        public ConfigureInfrastructureFirewallRulesCommand(ISystem system)
            : base(system)
        {
        }
    }
}
