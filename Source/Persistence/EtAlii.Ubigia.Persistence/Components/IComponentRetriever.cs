// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IComponentRetriever
    {
        IAsyncEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
                where T : CompositeComponent;

        Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent;
    }
}
