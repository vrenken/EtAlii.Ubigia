namespace EtAlii.Servus.Storage.InMemory
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryStorageFactory : StorageFactoryBase<InMemoryStorage>
    {
        public InMemoryStorageFactory(IStorageConfiguration storageConfiguration) 
            : base(storageConfiguration)
        {
        }

        public override IStorage Create(IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<InMemoryStorageSerializer, InMemoryFolderManager, InMemoryFileManager, InMemoryPathBuilder, InMemoryContainerProvider>(),
                new InMemoryFactoryScaffolding(),
            };

            return Create(diagnostics, scaffoldings);
        }
    }
}