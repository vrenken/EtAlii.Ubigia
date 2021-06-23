// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IEntryCacheGetHandler
    {
        Task<IReadOnlyEntry> Handle(Identifier identifier, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> Handle(IEnumerable<Identifier> identifiers, ExecutionScope scope);
    }
}