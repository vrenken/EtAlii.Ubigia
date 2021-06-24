// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class UpdateEntryFactory : IUpdateEntryFactory
    {
        private readonly IFabricContext _fabric;
        public UpdateEntryFactory(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public Task<IEditableEntry> Create(
            IReadOnlyEntry entry,
            ExecutionScope scope)
        {
            return Create(entry, entry.Tag, scope);
        }

        public async Task<IEditableEntry> Create(
            IReadOnlyEntry entry, 
            string tag,
            ExecutionScope scope)
        {
            var newEntry = await _fabric.Entries.Prepare().ConfigureAwait(false);
            newEntry.Type = entry.Type;
            newEntry.Tag = tag;
            newEntry.Downdate = Relation.NewRelation(entry.Id);
            newEntry = (IEditableEntry)await _fabric.Entries.Change(newEntry, scope).ConfigureAwait(false);
            return newEntry;
        }
    }
}
