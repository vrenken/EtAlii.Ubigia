namespace EtAlii.Ubigia.Storage
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IComponentStorage
    {
        ContainerIdentifier GetNextContainer(ContainerIdentifier container);

        T Retrieve<T>(ContainerIdentifier container) 
            where T : NonCompositeComponent;
        IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container) 
            where T : CompositeComponent;

        void Store<T>(ContainerIdentifier container, T component) 
            where T : class, IComponent;
        void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : class, IComponent;
    }
}
