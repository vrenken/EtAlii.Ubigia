namespace EtAlii.Servus.Storage.Ntfs
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;

    public class NtfsStorageFactory : StorageFactoryBase<NtfsStorage>
    {
        public NtfsStorageFactory(INtfsStorageConfiguration storageConfiguration) 
            : base(storageConfiguration)
        {
        }

        public override IStorage Create(IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<NtfsStorageSerializer, NtfsFolderManager, NtfsFileManager, NtfsPathBuilder, NtfsContainerProvider>(),
                new NtfsFactoryScaffolding(),
            };

            return Create(diagnostics, scaffoldings);
        }
    }
}