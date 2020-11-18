namespace EtAlii.xTechnology.Hosting
{
	public class TestHost : TestHostBase<TestHostManager>
	{
	    public TestHost(IHostConfiguration configuration, ISystemManager systemManager)
	        : base(configuration, systemManager)
	    {
	    }
    }
}