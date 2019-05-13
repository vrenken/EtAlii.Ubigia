namespace EtAlii.Ubigia.Infrastructure.Transport.Ntfs
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Hosting;

    public class StorageService : ServiceBase, IStorageService
    {
        public IStorage Storage { get; }

        public StorageService(IStorage storage)
        {
            Storage = storage;
        }

        public override Task Start()
        {
            // Handle Start.
            return Task.CompletedTask;
        }

        public override Task Stop()
        {
            // Handle Stop.
            return Task.CompletedTask;
        }

        protected override Task Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(StorageService));
            return Task.CompletedTask;
        }
    }
}
