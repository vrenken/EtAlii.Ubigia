namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Hosting.Server;

	public class TestHostManager : HostManagerBase
	{
		private IDisposable _server;
		
		protected override IWebHost CreateHost(IWebHostBuilder webHostBuilder, out bool hostIsAlreadyStarted)
		{
			hostIsAlreadyStarted = false;
			var host = webHostBuilder.Build();
			_server = (IDisposable)host.Services.GetService(typeof(IServer));
			return host;
		}
		
		public override async Task Stopped()
		{
			await base.Stopped().ConfigureAwait(false);

			_server.Dispose();
			_server = null;
		}
	}
}