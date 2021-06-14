// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest;
    using EtAlii.xTechnology.Threading;

    public class InProcessInfrastructureHostTestContext : RestHostTestContext
    {
        private readonly IContextCorrelator _contextCorrelator = new ContextCorrelator();

        public InProcessInfrastructureHostTestContext()
        {
            UseInProcessConnection = true;
        }

        public IRestInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(this, _contextCorrelator);
            var infrastructureClient = new RestInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }
    }
}
