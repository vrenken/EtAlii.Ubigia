namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class EntryCacheGetHandler
    {
        private readonly EntryCacheHelper _cacheHelper;
        private readonly EntryCacheContextProvider _contextProvider;

        public EntryCacheGetHandler(
            EntryCacheHelper cacheHelper,
            EntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public IReadOnlyEntry Handle(Identifier identifier)
        {
            var entry = _cacheHelper.GetEntry(identifier);
            if (entry == null)
            {
                entry = _contextProvider.Context.Get(identifier);
                _cacheHelper.StoreEntry(entry);
            }
            return entry;
        }

        public IEnumerable<IReadOnlyEntry> Handle(IEnumerable<Identifier> identifiers)
        {
            var entries = new List<IReadOnlyEntry>();
            var missingIdentifiers = new List<Identifier>();

            foreach (var identifier in identifiers)
            {
                var entry = _cacheHelper.GetEntry(identifier);
                if (entry == null)
                {
                    missingIdentifiers.Add(identifier);
                }
                else
                {
                    entries.Add(entry);
                }
            }
            if (missingIdentifiers.Count > 0)
            {
                var missingEntries = _contextProvider.Context.Get(missingIdentifiers);
                entries.AddRange(missingEntries);
                foreach (var missingEntry in missingEntries)
                {
                    _cacheHelper.StoreEntry(missingEntry);
                }
            }
            return entries;
        }
    }
}
