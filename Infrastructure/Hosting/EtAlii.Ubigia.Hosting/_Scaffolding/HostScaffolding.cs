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
            container.Register(() => _hostConfiguration.Storage);
            container.Register(() => _hostConfiguration.Infrastructure);
            container.Register(() => _hostConfiguration);

            //container.Register<ISerializer, Serializer>(Lifestyle.Singleton);
            container.Register(() => new SerializerFactory().Create());
        }
    }
}
