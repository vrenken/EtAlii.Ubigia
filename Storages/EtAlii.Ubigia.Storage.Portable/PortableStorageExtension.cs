namespace EtAlii.Ubigia.Storage.Portable
{
    using EtAlii.xTechnology.MicroContainer;
    using PCLStorage;

    public class PortableStorageExtension : IStorageExtension
    {
        private readonly IFolder _localStorage;

        public PortableStorageExtension(IFolder localStorage)
        {
            _localStorage = localStorage;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<PortableStorageSerializer, PortableFolderManager, PortableFileManager, PortablePathBuilder, PortableContainerProvider>(),
                new PortableFactoryScaffolding(_localStorage),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
