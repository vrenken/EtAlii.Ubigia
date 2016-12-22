namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageScaffolding : IScaffolding
    {
        private readonly IStorageConfiguration _storageConfiguration;

        public StorageScaffolding(IStorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;
        }

        public void Register(Container container)
        {
            container.Register<IStorageConfiguration>(() => _storageConfiguration);
            container.Register<ISerializer>(() => new SerializerFactory().Create());
        }
    }
}
