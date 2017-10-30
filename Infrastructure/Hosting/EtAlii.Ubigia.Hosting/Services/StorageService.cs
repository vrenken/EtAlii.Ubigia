namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using Storage;

    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public void Start()
        {
            // Nothing that we can do with a storage instance.
            // _storage 
        }

        public void Stop()
        {
            // Nothing that we can do with a storage instance.
            // _storage 
        }
    }
}
