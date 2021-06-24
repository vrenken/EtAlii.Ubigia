// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class IdentifierTailGetter : IIdentifierTailGetter
    {
        private readonly IIdentifierRootUpdater _rootUpdater;

        private readonly ILogicalContext _context;
        private readonly Dictionary<Guid, Identifier> _cachedTailIdentifiers;
        private readonly SemaphoreSlim _lockObject = new(1,1); // TODO: This lock-object should be shared with the head getter.

        public IdentifierTailGetter(
            IIdentifierRootUpdater rootUpdater, 
            ILogicalContext context)
        {
            _rootUpdater = rootUpdater;
            _context = context;
            _cachedTailIdentifiers = new Dictionary<Guid, Identifier>();
        }

        public async Task<Identifier> Get(Guid spaceId)
        {
            await _lockObject.WaitAsync().ConfigureAwait(false);
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
                _lockObject.Release();
            }
        }

        private async Task<Identifier> DetermineTail(Guid spaceId)
        {
            // load from root "Tail"

            var root = await _context.Roots.Get(spaceId, DefaultRoot.Tail).ConfigureAwait(false);
            var tailIdentifier = root?.Identifier ?? Identifier.Empty;

            if (tailIdentifier == Identifier.Empty)
            {
                // Determine from disk.
                tailIdentifier = DetermineTailFromDisk(spaceId);
                await _rootUpdater.Update(spaceId, DefaultRoot.Tail, tailIdentifier).ConfigureAwait(false);
            }
            return tailIdentifier;
        }

        private Identifier DetermineTailFromDisk(Guid spaceId)
        {
            var space = _context.Spaces.Get(spaceId);
            var storageId = _context.Storages.GetLocal().Id;
            var accountId = space.AccountId;
            return Identifier.Create(storageId, accountId, spaceId, 0, 0, 0);
        }

    }
}