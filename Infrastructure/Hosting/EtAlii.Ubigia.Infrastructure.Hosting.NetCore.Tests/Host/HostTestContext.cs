namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;

	public sealed class HostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
    }

    public partial class HostTestContext<TInfrastructureTestHost> : HostTestContextBase, IHostTestContext<TInfrastructureTestHost>
	    where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	}
}
