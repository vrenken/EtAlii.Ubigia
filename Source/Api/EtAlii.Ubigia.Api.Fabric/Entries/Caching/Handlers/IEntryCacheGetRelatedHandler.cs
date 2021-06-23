// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal interface IEntryCacheGetRelatedHandler
    {
        IAsyncEnumerable<IReadOnlyEntry> Handle(Identifier identifier, EntryRelation relations, ExecutionScope scope);
    }
}