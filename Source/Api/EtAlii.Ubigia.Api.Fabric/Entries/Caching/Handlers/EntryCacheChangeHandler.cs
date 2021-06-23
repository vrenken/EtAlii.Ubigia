// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class EntryCacheChangeHandler : IEntryCacheChangeHandler
    {
        private readonly IEntryCacheHelper _cacheHelper;
        private readonly IEntryCacheContextProvider _contextProvider;

        public EntryCacheChangeHandler(
            IEntryCacheHelper cacheHelper,
            IEntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<IReadOnlyEntry> Handle(IEditableEntry entry, ExecutionScope scope)
        {
            var changedEntry = await _contextProvider.Context.Change(entry, scope).ConfigureAwait(false);

            if (_cacheHelper.ShouldStore(changedEntry))
            {
                _cacheHelper.Store(changedEntry);
            }
            // TODO: Always clear related items from the cache.
            _cacheHelper.InvalidateRelated(changedEntry);

            // TODO: CACHING - Most probably the invalidateEntry should better be called on the entries as well.
            //scope.Cache.InvalidateEntry(changedEntry.Id)
            //scope.Cache.InvalidateEntry(entry.Id)

            return changedEntry;
        }
    }
}
