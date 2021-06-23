// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
                entry = await _contextProvider.Context.Get(identifier, scope).ConfigureAwait(false);
                if (_cacheHelper.ShouldStore(entry))
                {
                    _cacheHelper.Store(entry);
                }
            }
            return entry;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Handle(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
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
                    yield return entry;
                }
            }
            if (missingIdentifiers.Count > 0)
            {
                var missingEntries = _contextProvider.Context.Get(missingIdentifiers, scope);
                await foreach (var missingEntry in missingEntries.ConfigureAwait(false))
                {
                    if (_cacheHelper.ShouldStore(missingEntry))
                    {
                        _cacheHelper.Store(missingEntry);
                    }

                    yield return missingEntry;
                }
            }
        }
    }
}
