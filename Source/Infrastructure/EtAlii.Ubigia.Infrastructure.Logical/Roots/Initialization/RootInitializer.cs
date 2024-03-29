﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;

internal class RootInitializer : IRootInitializer
{
    private readonly IFabricContext _fabric;
    private readonly ILogicalEntrySet _entries;

    public RootInitializer(IFabricContext fabric, ILogicalEntrySet entries)
    {
        _fabric = fabric;
        _entries = entries;
    }

    /// <inheritdoc />
    public async Task Initialize(Guid storageId, Guid spaceId, Root root)
    {
        if (root.Identifier == Identifier.Empty)
        {
            var entry = (IEditableEntry)await _entries.Prepare(storageId, spaceId).ConfigureAwait(false);
            entry.Type = root.Name;

            //var tailRoot = Roots.Get(spaceId, DefaultRoot.Tail)
            //entry.Parent = Relation.NewRelation(tailRoot.Identifier)

            await _fabric.Entries.Store(entry).ConfigureAwait(false);
            root.Identifier = entry.Id;
            await _fabric.Roots.Update(spaceId, root.Id, root).ConfigureAwait(false);
        }
    }
}
