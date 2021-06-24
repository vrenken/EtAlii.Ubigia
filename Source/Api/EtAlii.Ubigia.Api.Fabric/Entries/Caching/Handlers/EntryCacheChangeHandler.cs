// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
            _cacheHelper.InvalidateRelated(changedEntry);

            return changedEntry;
        }
    }
}
