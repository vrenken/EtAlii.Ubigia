namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;

	public sealed class HostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
    }

    public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	}
}
