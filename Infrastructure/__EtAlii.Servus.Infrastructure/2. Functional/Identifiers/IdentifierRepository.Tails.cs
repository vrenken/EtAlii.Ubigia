namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api;

    internal partial class IdentifierRepository : IIdentifierRepository
    {
        public Identifier GetTail(Guid spaceId)
        {
            lock (_lockObject)
            {
                var tailIdentifier = Identifier.Empty;
                if (!_cachedTailIdentifiers.TryGetValue(spaceId, out tailIdentifier))
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

            var root = RootRepository.Get(spaceId, DefaultRoot.Tail);
            var tailIdentifier = root != null ? root.Identifier : Identifier.Empty;

            if (tailIdentifier == Identifier.Empty)
            {
                // Determine from disk.
                tailIdentifier = DetermineTailFromDisk(spaceId);
                UpdateRoot(spaceId, DefaultRoot.Tail, tailIdentifier);
            }
            return tailIdentifier;
        }

        private Identifier DetermineTailFromDisk(Guid spaceId)
        {
            var space = SpaceRepository.Get(spaceId);
            var storageId = StorageRepository.GetLocal().Id;
            var accountId = space.AccountId;
            return Identifier.Create(storageId, accountId, spaceId, 0, 0, 0);
        }
    }
}