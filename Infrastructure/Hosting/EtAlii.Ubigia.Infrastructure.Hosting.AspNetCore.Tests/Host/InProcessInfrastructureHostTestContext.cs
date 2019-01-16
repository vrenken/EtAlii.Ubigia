namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
        public IInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(Host.Server);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }
    }
}