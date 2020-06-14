namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;

	public partial class HostTestContext<TInfrastructureTestHost> 
		where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
        private const string HostSchemaAndIp = "http://127.0.0.1";

		public Uri HostAddress { get; } = new Uri(HostSchemaAndIp, UriKind.Absolute);

		public Uri ManagementServiceAddress { get; private set; }
		public Uri DataServiceAddress { get; private set; }
    }
}
