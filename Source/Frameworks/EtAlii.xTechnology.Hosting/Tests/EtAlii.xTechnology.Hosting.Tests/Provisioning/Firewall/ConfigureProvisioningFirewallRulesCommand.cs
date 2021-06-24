// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Firewall
{
    public class ConfigureProvisioningFirewallRulesCommand : ConfigureFirewallRulesCommandBase
    {
        public override string Name => "Admin/Configure firewall rules";

        public override string ScriptResourceName => "Provisioning.Firewall.ConfigureFirewall.ps1";

        public ConfigureProvisioningFirewallRulesCommand(ISystem system)
            : base(system)
        {
        }
    }
}
