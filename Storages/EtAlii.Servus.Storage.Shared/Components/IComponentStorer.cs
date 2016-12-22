namespace EtAlii.Servus.Storage
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IComponentStorer
    {
        void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent;
    }
}
