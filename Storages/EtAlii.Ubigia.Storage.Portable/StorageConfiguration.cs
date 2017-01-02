namespace EtAlii.Ubigia.Storage.Portable
{
    using EtAlii.Ubigia.Storage;

    public class StorageConfiguration : IStorageFactory
    {
        public string Name { get { return _name; } set { _name = value; } }
        private string _name;

        public IStorage Create()
        {
            var configuration = new EtAlii.Ubigia.Storage.StorageConfiguration()
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