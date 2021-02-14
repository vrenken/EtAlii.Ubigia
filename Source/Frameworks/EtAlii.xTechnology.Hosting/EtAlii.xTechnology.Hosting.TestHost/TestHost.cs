namespace EtAlii.xTechnology.Hosting
{
// This class is specific enough. We'll keep it's naming
#pragma warning disable CA1724
    public class TestHost : TestHostBase<TestHostManager>
#pragma warning restore CA1724
    {
        public TestHost(IHostConfiguration configuration, ISystemManager systemManager)
	        : base(configuration, systemManager)
	    {
	    }
    }
}
