namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    public class ConfigureFirewallRulesCommand : ConfigureFirewallRulesCommandBase
    {
        public override string Name => "Admin/Configure firewall rules";

        public override string ScriptResourceName => "Commands.Admin.Firewall.ConfigureFirewall.ps1";

        public ConfigureFirewallRulesCommand(ISystem system)
            : base(system)
        {
        }
    }
}
