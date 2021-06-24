// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    internal class NextContainerIdentifierFromFolderAlgorithm : INextContainerIdentifierAlgorithm
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ILatestEntryGetter _latestEntryGetter;
        private readonly IContainerProvider _containerProvider;
        private readonly object _lockObject = new();

        public NextContainerIdentifierFromFolderAlgorithm(
            IPathBuilder pathBuilder,
            ILatestEntryGetter latestEntryGetter,
            IContainerProvider containerProvider)
        {
            _pathBuilder = pathBuilder;
            _latestEntryGetter = latestEntryGetter;
            _containerProvider = containerProvider;
        }

        public ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier)
        {
            lock (_lockObject)
            {
                // We don't need the entriesFolder stored in currentContainerIdentifier.Paths[0].
                var storageFolder = currentContainerIdentifier.Paths[1];
                var accountFolder = currentContainerIdentifier.Paths[2];
                var spaceFolder = currentContainerIdentifier.Paths[3];

                ulong eraDelta = 0;
                ulong periodDelta = 0;
                ulong monentDelta = 1;

                var folderToInspect = _pathBuilder.GetFolder(currentContainerIdentifier);

                var eraFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, eraDelta);
                folderToInspect = _pathBuilder.Combine(folderToInspect, eraFolder);

                var periodFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, periodDelta);
                folderToInspect = _pathBuilder.Combine(folderToInspect, periodFolder);

                var momentFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, monentDelta);
                _pathBuilder.Combine(folderToInspect, momentFolder);

                var nextContainerIdentifier = _containerProvider.ForEntry(storageFolder, accountFolder, spaceFolder, eraFolder, periodFolder, momentFolder);
                return nextContainerIdentifier;
            }
        }
    }
}
