namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.xTechnology.Hosting;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
        public IInfrastructureClient CreateRestInfrastructureClient()
        {
            var httpClientFactory = new TestHttpClientFactory(this.Host.Server);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return infrastructureClient;
        }

    }
}