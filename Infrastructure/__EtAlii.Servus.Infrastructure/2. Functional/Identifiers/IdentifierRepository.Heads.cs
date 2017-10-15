namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api;

    internal partial class IdentifierRepository : IIdentifierRepository
    {
        private bool _headIsInitialized;

        public Identifier GetCurrentHead(Guid spaceId)
        {
            lock (_lockObject)
            {
                var headIdentifier = Identifier.Empty;
                if (!_cachedHeadIdentifiers.TryGetValue(spaceId, out headIdentifier))
                {
                    headIdentifier = DetermineHead(spaceId);
                    _cachedHeadIdentifiers[spaceId] = headIdentifier;
                }
                return headIdentifier;
            }
        }

        public Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            lock (_lockObject)
            {
                previousHeadIdentifier = GetCurrentHead(spaceId);

                var nextHeadIdentifier = GetNextIdentifier(spaceId, previousHeadIdentifier);

                UpdateRoot(spaceId, "Head", nextHeadIdentifier);
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
                var root = RootRepository.Get(spaceId, DefaultRoot.Head);
                headIdentifier = root != null ? root.Identifier : Identifier.Empty;

                if (headIdentifier == Identifier.Empty)
                {
                    // Determine from container storage.
                    headIdentifier = DetermineHeadFromComponentStorage(spaceId);
                    UpdateRoot(spaceId, DefaultRoot.Head, headIdentifier);
                }
            }
            else
            {
                // Determine from container storage.
                headIdentifier = DetermineHeadFromComponentStorage(spaceId);
                UpdateRoot(spaceId, DefaultRoot.Head, headIdentifier);
                _headIsInitialized = true;
            }

            return headIdentifier;
        }

        private Identifier DetermineHeadFromComponentStorage(Guid spaceId)
        {
            var space = SpaceRepository.Get(spaceId);
            var storageId = StorageRepository.GetLocal().Id;
            var accountId = space.AccountId;

            // Calculate identifier.
            var containerId = _storage.ContainerProvider.ForEntry(storageId, space.AccountId, spaceId);
            var newContainerId = _storage.Components.GetNextContainer(containerId);

            return _storage.ContainerProvider.ToIdentifier(storageId, space.AccountId, spaceId, newContainerId);
        }
    }
}