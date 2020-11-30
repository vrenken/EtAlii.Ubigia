namespace EtAlii.xTechnology.Hosting.Tests
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
