namespace EtAlii.Ubigia.Infrastructure.Transport.InMemory
{
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Hosting;

    public class StorageService : ServiceBase, IStorageService
    {
        public IStorage Storage { get; }

        public StorageService(IStorage storage)
        {
            Storage = storage;
        }

        public override void Start()
        {
            // Handle Start.
        }

        public override void Stop()
        {
            // Handle Stop.
        }

        protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(StorageService));
        }
    }
}
