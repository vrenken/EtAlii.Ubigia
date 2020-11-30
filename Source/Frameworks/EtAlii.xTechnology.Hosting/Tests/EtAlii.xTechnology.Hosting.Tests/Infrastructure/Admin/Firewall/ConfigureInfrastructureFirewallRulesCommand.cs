namespace EtAlii.xTechnology.Hosting.Tests
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
