namespace EtAlii.Servus.Storage
{
    using System;

    public interface IItemStorage
    {
        void Store<T>(T item, Guid id, ContainerIdentifier container) where T : class;
        T Retrieve<T>(Guid id, ContainerIdentifier container) where T : class;
        void Remove(Guid id, ContainerIdentifier container);
        Guid[] Get(ContainerIdentifier container);
        bool Has(Guid id, ContainerIdentifier container);
    }
}
