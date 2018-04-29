namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;

	public sealed class HostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
    }

    public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	}
}
