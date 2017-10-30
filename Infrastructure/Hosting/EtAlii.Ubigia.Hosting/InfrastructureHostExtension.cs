namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using Functional;
    using Storage;
    using xTechnology.Hosting;

    public class InfrastructureHostExtension : IHostExtension
    {
        private readonly IStorage _storage;
        private readonly IInfrastructure _infrastructure;
        //private readonly IHostConfiguration _configuration;

        public InfrastructureHostExtension(IStorage storage, IInfrastructure infrastructure)
        {
            _storage = storage;
            _infrastructure = infrastructure;
        }

        public void Register(Container container)
        {
            //var storage = _storageFactory.Create(_diagnostics);
            //var infrastructure = _infrastructureFactory.Create(storage, _diagnostics);
            container.Register(() => _storage);
            container.Register(() => _infrastructure);
            container.Register<IStorageService, StorageService>();
            container.Register<IInfrastructureService, InfrastructureService>();

            //container.Register<ISerializer, Serializer>(Lifestyle.Singleton);
            //container.Register(() => new SerializerFactory().Create());
        }
    }
}
