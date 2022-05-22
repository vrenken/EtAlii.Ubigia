// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IdentifierTailGetter : IIdentifierTailGetter
    {
        private readonly IIdentifierRootUpdater _rootUpdater;
        private readonly IIdentifierGetLocker _getLocker;

        private readonly ILogicalContext _context;
        private readonly Dictionary<Guid, Identifier> _cachedTailIdentifiers;

        public IdentifierTailGetter(
            IIdentifierRootUpdater rootUpdater,
            IIdentifierGetLocker getLocker,
            ILogicalContext context)
        {
            _rootUpdater = rootUpdater;
            _getLocker = getLocker;
            _context = context;
            _cachedTailIdentifiers = new Dictionary<Guid, Identifier>();
        }

        /// <inheritdoc />
        public async Task<Identifier> Get(Guid spaceId)
        {
            await _getLocker.LockObject.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!_cachedTailIdentifiers.TryGetValue(spaceId, out var tailIdentifier))
                {
                    tailIdentifier = await DetermineTail(spaceId).ConfigureAwait(false);
                    _cachedTailIdentifiers[spaceId] = tailIdentifier;
                }
                return tailIdentifier;
            }
            finally
            {
                _getLocker.LockObject.Release();
            }
        }

        private async Task<Identifier> DetermineTail(Guid spaceId)
        {
            // load from root "Tail"

            var root = await _context.Roots
                .Get(spaceId, DefaultRoot.Tail)
                .ConfigureAwait(false);
            var tailIdentifier = root?.Identifier ?? Identifier.Empty;

            if (tailIdentifier == Identifier.Empty)
            {
                // Determine from disk.
                tailIdentifier = await DetermineTailFromDisk(spaceId).ConfigureAwait(false);
                await _rootUpdater
                    .Update(spaceId, DefaultRoot.Tail, tailIdentifier)
                    .ConfigureAwait(false);
            }
            return tailIdentifier;
        }

        private async Task<Identifier> DetermineTailFromDisk(Guid spaceId)
        {
            var space = await _context.Spaces
                .Get(spaceId)
                .ConfigureAwait(false);
            var storage = await _context.Storages.GetLocal().ConfigureAwait(false);
            var storageId = storage.Id;
            var accountId = space.AccountId;
            return Identifier.Create(storageId, accountId, spaceId, 0, 0, 0);
        }

    }
}
