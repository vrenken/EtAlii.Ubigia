namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InfrastructureTestHost>
    {
        public IInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(Host.Server);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }
    }
}