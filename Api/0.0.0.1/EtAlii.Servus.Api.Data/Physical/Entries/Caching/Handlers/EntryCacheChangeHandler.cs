namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class EntryCacheChangeHandler
    {
        private readonly EntryCacheHelper _cacheHelper;
        private readonly EntryCacheContextProvider _contextProvider;

        public EntryCacheChangeHandler(
            EntryCacheHelper cacheHelper,
            EntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public IReadOnlyEntry Handle(IEditableEntry entry)
        {
            var changedEntry = _contextProvider.Context.Change(entry);

            _cacheHelper.StoreEntry(changedEntry);

            // TODO: Always clear related items from the cache.
            _cacheHelper.InvalidateRelated(changedEntry);

            return changedEntry;
        }
    }
}
