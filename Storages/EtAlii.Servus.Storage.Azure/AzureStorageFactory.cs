namespace EtAlii.Servus.Storage.Azure
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;

    public class AzureStorageFactory : StorageFactoryBase<AzureStorage>
    {
        public AzureStorageFactory(IStorageConfiguration storageConfiguration)
            : base(storageConfiguration)
        {
        }

        public override IStorage Create(IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<AzureStorageSerializer, AzureFolderManager, AzureFileManager, AzurePathBuilder, AzureContainerProvider>(),
                new AzureFactoryScaffolding(),
            };

            return Create(diagnostics, scaffoldings);
        }
    }
}
