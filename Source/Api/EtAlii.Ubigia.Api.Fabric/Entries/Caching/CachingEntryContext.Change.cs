// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal partial class CachingEntryContext
    {
        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var changedEntry = await _decoree
                .Change(entry, scope)
                .ConfigureAwait(false);

            if (ShouldStore(changedEntry))
            {
                scope.EntryCache[changedEntry.Id] = changedEntry;
            }
            InvalidateRelated(changedEntry, scope);

            return changedEntry;
        }
    }
}
