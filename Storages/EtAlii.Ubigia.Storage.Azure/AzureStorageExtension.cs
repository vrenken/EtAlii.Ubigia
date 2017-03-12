namespace EtAlii.Ubigia.Storage.Azure
{
    using EtAlii.xTechnology.MicroContainer;

    public class AzureStorageExtension : IStorageExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<AzureStorageSerializer, AzureFolderManager, AzureFileManager, AzurePathBuilder, AzureContainerProvider>(),
                new AzureFactoryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
