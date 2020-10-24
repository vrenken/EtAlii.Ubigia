﻿namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;

    internal class ComponentStorage : IComponentStorage
    {
        private readonly IComponentStorer _componentStorer;
        private readonly ICompositeComponentStorer _compositeComponentStorer;
        private readonly IComponentRetriever _componentRetriever;
        private readonly INextContainerIdentifierAlgorithm _nextContainerIdentifierAlgorithm;
        private readonly IContainerCreator _containerCreator;

        public ComponentStorage(
            IComponentStorer componentStorer, 
            ICompositeComponentStorer compositeComponentStorer,
            IComponentRetriever componentRetriever, 
            INextContainerIdentifierAlgorithm nextContainerIdentifierAlgorithm, 
            IContainerCreator containerCreator)
        {
            _componentStorer = componentStorer;
            _compositeComponentStorer = compositeComponentStorer;
            _componentRetriever = componentRetriever;
            _nextContainerIdentifierAlgorithm = nextContainerIdentifierAlgorithm;
            _containerCreator = containerCreator;
        }

        public ContainerIdentifier GetNextContainer(ContainerIdentifier container)
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new StorageException("No current container specified");
            }

            try
            {
                var nextContainerIdentifier = _nextContainerIdentifierAlgorithm.Create(container);
                _containerCreator.Create(nextContainerIdentifier);
                return nextContainerIdentifier;
            }
            catch (Exception e)
            {
                throw new StorageException("Unable get the next container", e);
            }
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new StorageException("No container specified");
            }

            try
            {
                return _componentRetriever.RetrieveAll<T>(container);
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new StorageException("No container specified");
            }

            try
            {
                return _componentRetriever.Retrieve<T>(container);
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }

        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : class, IComponent
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new StorageException("No container specified");
            }

            try
            {
                foreach (var component in components)
                {
                    Store(container, component);
                }
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to store components in the specified container", e);
            }
        }

        public void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new StorageException("No container specified");
            }

            try
            {
                if (component is CompositeComponent compositeComponent)
                {
                    _compositeComponentStorer.Store(container, compositeComponent);
                }
                else
                {
                    _componentStorer.Store(container, component);
                }
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to store component in the specified container", e);
            }
        }
    }
}