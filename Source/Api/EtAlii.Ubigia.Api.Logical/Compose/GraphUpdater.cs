// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class GraphUpdater : IGraphUpdater
    {
        private readonly IFabricContext _fabric;

        public GraphUpdater(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<IEditableEntry> Update(IReadOnlyEntry entry, string newType, ExecutionScope scope)
        {
            var updateEntry = await _fabric.Entries.Prepare().ConfigureAwait(false);
            updateEntry.Type = newType;
            updateEntry.Tag = entry.Tag;
            updateEntry.Downdate = Relation.NewRelation(entry.Id);
            updateEntry = (IEditableEntry)await _fabric.Entries.Change(updateEntry, scope).ConfigureAwait(false);
            return updateEntry;
        }

        public async Task<IEditableEntry> Update(IReadOnlyEntry entry, ExecutionScope scope)
        {
            return await Update(entry, entry.Type, scope).ConfigureAwait(false);
        }
    }
}
