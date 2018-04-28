namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	public sealed class HostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
    }

    public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	}
}
