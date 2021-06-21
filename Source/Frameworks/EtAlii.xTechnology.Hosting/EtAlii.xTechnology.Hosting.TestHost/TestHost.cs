namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;

    // This class is specific enough. We'll keep it's naming
#pragma warning disable CA1724
    public class TestHost : TestHostBase<TestHostManager>, ITestHost
#pragma warning restore CA1724
    {
        // This class should be public.
        public TestHost(IHostConfiguration configuration, ISystemManager systemManager)
	        : base(configuration, systemManager)
        {
        }

        public HttpMessageHandler CreateHandler() => Manager.TestServer.CreateHandler();

        public HttpClient CreateClient() => Manager.TestServer.CreateClient();
    }
}
