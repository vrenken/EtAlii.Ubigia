// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;

    // This class is specific enough. We'll keep it's naming
#pragma warning disable CA1724
    public class TestHost : TestHostBase<TestHostManager>, ITestHost
#pragma warning restore CA1724
    {
        // Warning: This class should keep its public constructor.
        // ReSharper disable once MemberCanBeProtected.Global
        public TestHost(IHostOptions options, ISystemManager systemManager)
	        : base(options, systemManager)
        {
        }

        public HttpMessageHandler CreateHandler() => Manager.TestServer.CreateHandler();

        public HttpClient CreateClient() => Manager.TestServer.CreateClient();
    }
}
