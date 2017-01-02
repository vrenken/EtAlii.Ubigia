namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface IComponentRetriever
    {
        IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
                where T : CompositeComponent;

        T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent;
    }
}
