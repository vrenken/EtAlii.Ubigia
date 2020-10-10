namespace EtAlii.xTechnology.Hosting
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Hosting.Server;
	using Microsoft.AspNetCore.Server.Kestrel.Core;

	public class TestHostManager : HostManagerBase
	{
		public KestrelServer Server { get; private set; }
		
		protected override IWebHost CreateHost(IWebHostBuilder webHostBuilder, out bool hostIsAlreadyStarted)
		{
			hostIsAlreadyStarted = false;
			var host = webHostBuilder.Build();
			Server = (KestrelServer)host.Services.GetService(typeof(IServer));
			return host;
		}
		
		public override async Task Stopped()
		{
			await base.Stopped();

			Server.Dispose();
			Server = null;
		}
	}
}