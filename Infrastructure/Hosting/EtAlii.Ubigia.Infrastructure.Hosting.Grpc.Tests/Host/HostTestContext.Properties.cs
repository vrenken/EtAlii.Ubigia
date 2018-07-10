namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;

	public partial class HostTestContext<TInfrastructureTestHost> 
		where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
        public TInfrastructureTestHost Host { get; private set; }

		private const string HostSchemaAndIp = "http://127.0.0.1";

		public Uri HostAddress { get; } = new Uri(HostSchemaAndIp, UriKind.Absolute);

	    public Uri ManagementServiceAddress => new Uri($"{HostSchemaAndIp}:{Host.AdminModule.Port}/Admin");
	    public Uri DataServiceAddress => new Uri($"{HostSchemaAndIp}:{Host.UserModule.Port}/User");
    }
}
