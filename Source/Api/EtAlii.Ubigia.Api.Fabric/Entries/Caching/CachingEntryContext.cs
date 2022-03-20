// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Linq;
    using System.Threading.Tasks;

    internal partial class CachingEntryContext : IEntryContext
    {
        private readonly IEntryContext _decoree;

        public CachingEntryContext(IEntryContext decoree)
        {
            _decoree = decoree;
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _decoree
                .Prepare()
                .ConfigureAwait(false);
        }

        private bool ShouldStore(IReadOnlyEntry entry)
        {
            return entry.Updates.Any();
        }
    }
}
