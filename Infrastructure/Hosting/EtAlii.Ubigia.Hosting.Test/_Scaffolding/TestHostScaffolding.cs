namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Tests;
    using SimpleInjector;
    using IScaffolding = EtAlii.Ubigia.Infrastructure.Hosting.IScaffolding;

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
