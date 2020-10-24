namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class IdentifierHeadGetter : IIdentifierHeadGetter
    {
        private readonly INextIdentifierGetter _nextIdentifierGetter;
        private readonly IIdentifierRootUpdater _rootUpdater;

        private readonly ILogicalContext _context;
        private readonly IFabricContext _fabric;

        private readonly Dictionary<Guid, Identifier> _cachedHeadIdentifiers;
        private readonly object _lockObject = new object(); // TODO: This lockobject should be shared with the tail getter.

        private bool _headIsInitialized;

        public IdentifierHeadGetter(
            INextIdentifierGetter nextIdentifierGetter, 
            IIdentifierRootUpdater rootUpdater, 
            ILogicalContext context,
            IFabricContext fabric)
        {
            _nextIdentifierGetter = nextIdentifierGetter;
            _rootUpdater = rootUpdater;
            _context = context;
            _fabric = fabric;
            _cachedHeadIdentifiers = new Dictionary<Guid, Identifier>();
        }

        public Identifier GetCurrent(Guid spaceId)
        {
            lock (_lockObject)
            {
                if (!_cachedHeadIdentifiers.TryGetValue(spaceId, out var headIdentifier))
                {
                    headIdentifier = DetermineHead(spaceId);
                    _cachedHeadIdentifiers[spaceId] = headIdentifier;
                }
                return headIdentifier;
            }
        }

        public Identifier GetNext(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            lock (_lockObject)
            {
                previousHeadIdentifier = GetCurrent(spaceId);

                var nextHeadIdentifier = _nextIdentifierGetter.GetNext(spaceId, previousHeadIdentifier);

                _rootUpdater.Update(spaceId, "Head", nextHeadIdentifier);
                _cachedHeadIdentifiers[spaceId] = nextHeadIdentifier;

                return nextHeadIdentifier;
            }
        }

        private Identifier DetermineHead(Guid spaceId)
        {
            Identifier headIdentifier;

            if (_headIsInitialized)
            {
                // load from root "Head"
                var root = _context.Roots.Get(spaceId, DefaultRoot.Head);
                headIdentifier = root != null ? root.Identifier : Identifier.Empty;

                if (headIdentifier == Identifier.Empty)
                {
                    // Determine from container storage.
                    headIdentifier = DetermineHeadFromComponentStorage(spaceId);
                    _rootUpdater.Update(spaceId, DefaultRoot.Head, headIdentifier);
                }
            }
            else
            {
                // Determine from container storage.
                headIdentifier = DetermineHeadFromComponentStorage(spaceId);
                _rootUpdater.Update(spaceId, DefaultRoot.Head, headIdentifier);
                _headIsInitialized = true;
            }

            return headIdentifier;
        }

        private Identifier DetermineHeadFromComponentStorage(Guid spaceId)
        {
            var space = _context.Spaces.Get(spaceId);
            var storageId = _context.Storages.GetLocal().Id;
            var accountId = space.AccountId;

            return _fabric.Identifiers.GetNextIdentifierFromStorage(storageId, accountId, spaceId);
        }
    }
}