namespace EtAlii.Ubigia.Persistence.InMemory
{
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryStorageExtension : IStorageExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<InMemoryStorageSerializer, InMemoryFolderManager, InMemoryFileManager, InMemoryPathBuilder, InMemoryContainerProvider>(),
                new InMemoryFactoryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
