namespace EtAlii.xTechnology.Hosting
{
	using Microsoft.AspNetCore.Hosting;

	public class HostManager : HostManagerBase
    {
	    protected override IWebHost CreateHost(IWebHostBuilder webHostBuilder, out bool hostIsAlreadyStarted)
	    {
		    hostIsAlreadyStarted = false;
		    return webHostBuilder.Build();
	    }
	}
}