// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;

    public class InProcessInfrastructureHostTestContext : HostTestContext
    {
        public IInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(this);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }
    }
}