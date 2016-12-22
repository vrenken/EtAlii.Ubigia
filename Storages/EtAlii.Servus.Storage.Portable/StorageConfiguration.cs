namespace EtAlii.Servus.Storage.Portable
{
    using EtAlii.Servus.Storage;

    public class StorageConfiguration : IStorageFactory
    {
        public string Name { get { return _name; } set { _name = value; } }
        private string _name;

        public IStorage Create()
        {
            var configuration = new EtAlii.Servus.Storage.StorageConfiguration()
                .Use(Name)
                .UsePortableStorage();

            return new StorageFactory().Create(configuration);
        }

        public IStorage Create(IStorageConfiguration configuration)
        {
            return new StorageFactory().Create(configuration);
        }
    }
}