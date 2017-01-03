namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.MicroContainer;

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
            container.Register<IStorage>(() => _hostConfiguration.Storage);
            container.Register<IInfrastructure>(() => _hostConfiguration.Infrastructure);
            container.Register<IHostConfiguration>(() => _hostConfiguration);

            //container.Register<ISerializer, Serializer>(Lifestyle.Singleton);
            container.Register<ISerializer>(() => new SerializerFactory().Create());
        }
    }
}
