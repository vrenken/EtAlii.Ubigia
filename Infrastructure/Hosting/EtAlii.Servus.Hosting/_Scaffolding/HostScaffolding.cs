namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;
    using SimpleInjector;

    public class HostScaffolding : IScaffolding
    {
        private readonly IHostConfiguration _hostConfiguration;

        public HostScaffolding(
            IHostConfiguration hostConfiguration)
        {
            _hostConfiguration = hostConfiguration;
        }

        public void Register(Container container)
        {
            //var storage = _storageFactory.Create(_diagnostics);
            //var infrastructure = _infrastructureFactory.Create(storage, _diagnostics);
            container.Register<IStorage>(() => _hostConfiguration.Storage, Lifestyle.Singleton);
            container.Register<IInfrastructure>(() => _hostConfiguration.Infrastructure, Lifestyle.Singleton);
            container.Register<IHostConfiguration>(() => _hostConfiguration, Lifestyle.Singleton);

            //container.Register<ISerializer, Serializer>(Lifestyle.Singleton);
            container.Register<ISerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);
        }
    }
}
