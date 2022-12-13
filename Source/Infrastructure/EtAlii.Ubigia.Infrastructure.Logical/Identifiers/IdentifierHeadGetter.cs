// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class IdentifierHeadGetter : IIdentifierHeadGetter
    {
        private readonly INextIdentifierGetter _nextIdentifierGetter;
        private readonly IIdentifierGetLocker _getLocker;
        private readonly IIdentifierRootUpdater _rootUpdater;

        private readonly ILogicalContext _context;
        private readonly IFabricContext _fabric;

        private readonly Dictionary<Guid, Identifier> _cachedHeadIdentifiers;

        private bool _headIsInitialized;

        public IdentifierHeadGetter(
            INextIdentifierGetter nextIdentifierGetter,
            IIdentifierGetLocker getLocker,
            IIdentifierRootUpdater rootUpdater,
            ILogicalContext context,
            IFabricContext fabric)
        {
            _nextIdentifierGetter = nextIdentifierGetter;
            _getLocker = getLocker;
            _rootUpdater = rootUpdater;
            _context = context;
            _fabric = fabric;
            _cachedHeadIdentifiers = new Dictionary<Guid, Identifier>();
        }

        /// <inheritdoc />
        public async Task<Identifier> GetCurrent(Guid spaceId)
        {
            await _getLocker.LockObject.WaitAsync().ConfigureAwait(false);
            try
            {
                return await GetCurrentInternal(spaceId).ConfigureAwait(false);
            }
            finally
            {
                _getLocker.LockObject.Release();
            }
        }

        private async Task<Identifier> GetCurrentInternal(Guid spaceId)
        {
            if (!_cachedHeadIdentifiers.TryGetValue(spaceId, out var headIdentifier))
            {
                headIdentifier = await DetermineHead(spaceId).ConfigureAwait(false);
                _cachedHeadIdentifiers[spaceId] = headIdentifier;
            }

            return headIdentifier;
        }

        /// <inheritdoc />
        public async Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNext(Guid spaceId)
        {
            await _getLocker.LockObject.WaitAsync().ConfigureAwait(false);
            try
            {
                var previousHeadIdentifier = await GetCurrentInternal(spaceId).ConfigureAwait(false);

                var nextHeadIdentifier = await _nextIdentifierGetter.GetNext(spaceId, previousHeadIdentifier).ConfigureAwait(false);

                await _rootUpdater
                    .Update(spaceId, PositionalRoot.Head, nextHeadIdentifier)
                    .ConfigureAwait(false);
                _cachedHeadIdentifiers[spaceId] = nextHeadIdentifier;

                return (NextHeadIdentifier: nextHeadIdentifier, PreviousHeadIdentifier: previousHeadIdentifier);
            }
            finally
            {
                _getLocker.LockObject.Release();
            }
        }

        private async Task<Identifier> DetermineHead(Guid spaceId)
        {
            Identifier headIdentifier;

            if (_headIsInitialized)
            {
                // load from root "Head"
                var root = await _context.Roots.Get(spaceId, PositionalRoot.Head.Name).ConfigureAwait(false);
                headIdentifier = root?.Identifier ?? Identifier.Empty;

                if (headIdentifier == Identifier.Empty)
                {
                    // Determine from container storage.
                    headIdentifier = await DetermineHeadFromComponentStorage(spaceId).ConfigureAwait(false);
                    await _rootUpdater.Update(spaceId, PositionalRoot.Head, headIdentifier).ConfigureAwait(false);
                }
            }
            else
            {
                // Determine from container storage.
                headIdentifier = await DetermineHeadFromComponentStorage(spaceId).ConfigureAwait(false);
                await _rootUpdater.Update(spaceId, PositionalRoot.Head, headIdentifier).ConfigureAwait(false);
                _headIsInitialized = true;
            }

            return headIdentifier;
        }

        private async Task<Identifier> DetermineHeadFromComponentStorage(Guid spaceId)
        {
            var space = await _context.Spaces.Get(spaceId).ConfigureAwait(false);
            var storage = await _context.Storages.GetLocal().ConfigureAwait(false);
            var storageId = storage.Id;
            var accountId = space.AccountId;

            return await _fabric.Identifiers.GetNextIdentifierFromStorage(storageId, accountId, spaceId).ConfigureAwait(false);
        }
    }
}
