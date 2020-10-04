namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;

    public class IdentifierTailGetter : IIdentifierTailGetter
    {
        private readonly IIdentifierRootUpdater _rootUpdater;

        private readonly ILogicalContext _context;
        private readonly Dictionary<Guid, Identifier> _cachedTailIdentifiers;
        private readonly object _lockObject = new object();

        public IdentifierTailGetter(
            IIdentifierRootUpdater rootUpdater, 
            ILogicalContext context)
        {
            _rootUpdater = rootUpdater;
            _context = context;
            _cachedTailIdentifiers = new Dictionary<Guid, Identifier>();
        }

        public Identifier Get(Guid spaceId)
        {
            lock (_lockObject)
            {
                if (!_cachedTailIdentifiers.TryGetValue(spaceId, out var tailIdentifier))
                {
                    tailIdentifier = DetermineTail(spaceId);
                    _cachedTailIdentifiers[spaceId] = tailIdentifier;
                }
                return tailIdentifier;
            }
        }

        private Identifier DetermineTail(Guid spaceId)
        {
            // load from root "Tail"

            var root = _context.Roots.Get(spaceId, DefaultRoot.Tail);
            var tailIdentifier = root != null ? root.Identifier : Identifier.Empty;

            if (tailIdentifier == Identifier.Empty)
            {
                // Determine from disk.
                tailIdentifier = DetermineTailFromDisk(spaceId);
                _rootUpdater.Update(spaceId, DefaultRoot.Tail, tailIdentifier);
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