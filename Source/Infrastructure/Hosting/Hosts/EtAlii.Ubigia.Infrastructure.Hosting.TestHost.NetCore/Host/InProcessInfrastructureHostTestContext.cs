// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;
    using EtAlii.xTechnology.Threading;

    public class InProcessInfrastructureHostTestContext : HostTestContext
    {
        private readonly IContextCorrelator _contextCorrelator = new ContextCorrelator();

        public IInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(this, _contextCorrelator);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }
    }
}
