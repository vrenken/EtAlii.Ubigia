namespace EtAlii.Ubigia.Storage
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IComponentRetriever
    {
        IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
                where T : CompositeComponent;

        T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent;
    }
}
