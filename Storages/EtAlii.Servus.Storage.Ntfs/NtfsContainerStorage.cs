namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public partial class NtfsContainerStorage : IContainerStorage
    {
        private readonly IComponentStorer _componentStorer;
        private readonly IComponentRetriever _componentRetriever;
        private readonly INextContainerIdentifierAlgorithm _nextContainerIdentifierAlgorithm;
        private readonly IContainerCreator _containerCreator;

        public NtfsContainerStorage(IComponentStorer componentStorer, IComponentRetriever componentRetriever, INextContainerIdentifierAlgorithm nextContainerIdentifierAlgorithm, IContainerCreator containerCreator)
        {
            _componentStorer = componentStorer;
            _componentRetriever = componentRetriever;
            _nextContainerIdentifierAlgorithm = nextContainerIdentifierAlgorithm;
            _containerCreator = containerCreator;
        }

        public ContainerIdentifier GetNextContainer(ContainerIdentifier currentContainerIdentifier)
        {
            var nextContainerIdentifier = _nextContainerIdentifierAlgorithm.Create(currentContainerIdentifier);
            _containerCreator.Create(nextContainerIdentifier);
            return nextContainerIdentifier;
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            return _componentRetriever.RetrieveAll<T>(container);
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            return _componentRetriever.Retrieve<T>(container);
        }

        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : EntryComponent
        {
            _componentStorer.StoreAll<T>(container, components);
        }

        public void Store<T>(ContainerIdentifier container, T component)
            where T : EntryComponent
        {
            _componentStorer.Store<T>(container, component);
        }
    }
}
