namespace EtAlii.xTechnology.Hosting
{
	using Microsoft.AspNetCore.Server.Kestrel.Core;

	public class TestHost : TestHostBase<TestHostManager>
	{
		public KestrelServer Server => Manager.Server;

	    public TestHost(IHostConfiguration configuration, ISystemManager systemManager)
	        : base(configuration, systemManager)
	    {
	    }
    }
}