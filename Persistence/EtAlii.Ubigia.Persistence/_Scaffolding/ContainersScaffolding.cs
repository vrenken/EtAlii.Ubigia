namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class ContainersScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IContainerCreator, ContainerCreator>();
            container.Register<INextContainerIdentifierAlgorithm, NextContainerIdentifierFromFolderAlgorithm>();
            container.RegisterDecorator(typeof(INextContainerIdentifierAlgorithm), typeof(NextContainerIdentifierFromTimeAlgorithm));
            container.Register<ILatestEntryGetter, LatestEntryGetter>();
        }
    }
}
