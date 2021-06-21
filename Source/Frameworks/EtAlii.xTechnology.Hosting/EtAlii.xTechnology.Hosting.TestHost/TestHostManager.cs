namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.TestHost;

    public class TestHostManager : HostManagerBase
	{
		private IDisposable _server;
        public TestServer TestServer { get; private set; }

        public TestHostManager()
        {
            ConfigureHost += webHost =>
            {
                webHost.UseTestServer();
            };
            ConfigureKestrel += options =>
            {
                options.Limits.MaxRequestBodySize = 1024 * 1024 * 2;
                options.Limits.MaxRequestBufferSize = 1024 * 1024 * 2;
                options.Limits.MaxResponseBufferSize = 1024 * 1024 * 2;
                options.AllowSynchronousIO = true;
            };
        }

		protected override IWebHost CreateHost(IWebHostBuilder webHostBuilder, out bool hostIsAlreadyStarted)
		{
			hostIsAlreadyStarted = false;
			var host = webHostBuilder.Build();

            TestServer = host.GetTestServer();
            TestServer.PreserveExecutionContext = false;
            TestServer.AllowSynchronousIO = true;
			_server = (IDisposable)host.Services.GetService(typeof(IServer));
			return host;
		}

		public override async Task Stopped()
		{
			await base.Stopped().ConfigureAwait(false);

            TestServer.Dispose();
            TestServer = null;
			_server.Dispose();
			_server = null;
		}
	}
}
