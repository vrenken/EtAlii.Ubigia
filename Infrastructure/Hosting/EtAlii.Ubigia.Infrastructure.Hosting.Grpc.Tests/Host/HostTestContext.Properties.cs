namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;

	public partial class HostTestContext
    {
        private const string HostSchemaAndIp = "http://127.0.0.1";

		public Uri HostAddress { get; } = new Uri(HostSchemaAndIp, UriKind.Absolute);

		public Uri ManagementApiAddress { get; private set; }
		public Uri DataApiAddress { get; private set; }
    }
}
