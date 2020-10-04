namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class EntryCacheGetHandler : IEntryCacheGetHandler
    {
        private readonly IEntryCacheHelper _cacheHelper;
        private readonly IEntryCacheContextProvider _contextProvider;

        public EntryCacheGetHandler(
            IEntryCacheHelper cacheHelper,
            IEntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<IReadOnlyEntry> Handle(Identifier identifier, ExecutionScope scope)
        {
            var entry = _cacheHelper.Get(identifier);
            if (entry == null)
            {
                entry = await _contextProvider.Context.Get(identifier, scope);
                if (_cacheHelper.ShouldStore(entry))
                {
                    _cacheHelper.Store(entry);
                }
            }
            return entry;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Handle(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            var entries = new List<IReadOnlyEntry>();
            var missingIdentifiers = new List<Identifier>();

            foreach (var identifier in identifiers)
            {
                var entry = _cacheHelper.Get(identifier);
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
                var missingEntries = await _contextProvider.Context.Get(missingIdentifiers, scope);
                entries.AddRange(missingEntries);
                foreach (var missingEntry in missingEntries)
                {
                    if (_cacheHelper.ShouldStore(missingEntry))
                    {
                        _cacheHelper.Store(missingEntry);
                    }
                }
            }
            return entries;
        }
    }
}
