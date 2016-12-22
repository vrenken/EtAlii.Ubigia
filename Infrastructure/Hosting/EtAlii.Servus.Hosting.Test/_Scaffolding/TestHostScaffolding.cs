namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Tests;
    using SimpleInjector;
    using IScaffolding = EtAlii.Servus.Infrastructure.Hosting.IScaffolding;

    public class TestHostScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>(Lifestyle.Singleton);
            container.Register<IInfrastructureClient>(() => CreateTestInfrastructureClient(container), Lifestyle.Singleton);
        }

        private IInfrastructureClient CreateTestInfrastructureClient(Container container)
        {
            var infrastructure = (TestInfrastructure)container.GetInstance<IInfrastructure>();
            var httpClientFactory = new TestHttpClientFactory(infrastructure);
            return new DefaultInfrastructureClient(httpClientFactory);
        }
    }
}
