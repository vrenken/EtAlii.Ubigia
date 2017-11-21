namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using Storage;

    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;
        private IHost _host;
        public HostStatus Status { get; } = new HostStatus(nameof(StorageService));
        public IHostCommand[] Commands { get; } = Array.Empty<IHostCommand>();

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
