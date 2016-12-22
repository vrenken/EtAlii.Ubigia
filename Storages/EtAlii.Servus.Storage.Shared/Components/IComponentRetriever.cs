namespace EtAlii.Servus.Storage
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IComponentRetriever
    {
        IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
                where T : CompositeComponent;

        T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent;
    }
}
