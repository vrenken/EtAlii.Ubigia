namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Functional;

	public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
        public IInfrastructure Infrastructure { get; private set; }
        public TInfrastructureTestHost Host { get; private set; }
        public string SystemAccountName { get; private set; }
		public string SystemAccountPassword { get; private set; }
	    public string TestAccountName { get; private set; }
	    public string TestAccountPassword { get; private set; }

	    public string AdminAccountName { get; private set; }
	    public string AdminAccountPassword { get; private set; }

		private const string HostSchemaAndIp = "http://127.0.0.1";

		public Uri HostAddress { get; } = new Uri(HostSchemaAndIp, UriKind.Absolute);

	    public Uri ManagementServiceAddress => new Uri($"{HostSchemaAndIp}:{Host.AdminModule.Port}/Admin");
	    public Uri DataServiceAddress => new Uri($"{HostSchemaAndIp}:{Host.UserModule.Port}/User");

		public string HostName => Infrastructure?.Configuration?.Name;
    }
}
