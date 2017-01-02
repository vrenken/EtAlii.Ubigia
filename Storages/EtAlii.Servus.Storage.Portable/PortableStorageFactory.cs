namespace EtAlii.Servus.Storage.Portable
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;
    using PCLStorage;

    public class PortableStorageFactory : StorageFactoryBase<PortableStorage>
    {
        private readonly IFolder _localStorage;

        public PortableStorageFactory(IStorageConfiguration configuration)
            : base(configuration)
        {
            _localStorage = FileSystem.Current.LocalStorage;
        }

        internal PortableStorageFactory(IStorageConfiguration configuration, IFolder localStorage)
            : base(configuration)
        {
            _localStorage = localStorage;
        }

        public override IStorage Create(IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<PortableStorageSerializer, PortableFolderManager, PortableFileManager, PortablePathBuilder, PortableContainerProvider>(),
                new PortableFactoryScaffolding(_localStorage),
            };

            return Create(diagnostics, scaffoldings);
        }
    }
}