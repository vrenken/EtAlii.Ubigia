namespace EtAlii.Servus.PowerShell.IntegrationTests
{
    using EtAlii.Servus.Infrastructure.Hosting.Tests;


    public class PowerShellTestHostedInfrastructure : TestHostedInfrastructureBase
    {
        public PowerShellTestHostedInfrastructure()
            : base("Unit test storage - PowerShell")
        {
        }
    }
}
