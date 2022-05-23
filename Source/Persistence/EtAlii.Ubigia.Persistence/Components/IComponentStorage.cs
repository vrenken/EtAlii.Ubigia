// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IComponentStorage
    {
        ContainerIdentifier GetNextContainer(ContainerIdentifier container);

        Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent;
        IAsyncEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent;

        void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent;
        Task StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : class, IComponent;
    }
}
