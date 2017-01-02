namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface IComponentStorer
    {
        void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : EntryComponent;

        void Store<T>(ContainerIdentifier container, T component)
            where T : EntryComponent;
    }
}
