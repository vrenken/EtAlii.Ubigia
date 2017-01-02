namespace EtAlii.Ubigia.Storage
{
    using System;

    internal class NextContainerIdentifierFromFolderAlgorithm : INextContainerIdentifierAlgorithm
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ILatestEntryGetter _latestEntryGetter;
        private readonly IContainerProvider _containerProvider;
        private readonly IImmutableFileManager _fileManager;
        private readonly object _lockObject = new object();

        public NextContainerIdentifierFromFolderAlgorithm(
            IImmutableFileManager fileManager, 
            IPathBuilder pathBuilder, 
            ILatestEntryGetter latestEntryGetter,
            IContainerProvider containerProvider)
        {
            _fileManager = fileManager;
            _pathBuilder = pathBuilder;
            _latestEntryGetter = latestEntryGetter;
            _containerProvider = containerProvider;
        }

        public ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier)
        {
            lock (_lockObject)
            {
                //var entriesFolder = currentContainerIdentifier.Paths[0];
                var storageFolder = currentContainerIdentifier.Paths[1];
                var accountFolder = currentContainerIdentifier.Paths[2];
                var spaceFolder = currentContainerIdentifier.Paths[3];

                UInt64 eraDelta = 0;
                UInt64 periodDelta = 0;
                UInt64 monentDelta = 1;

                var folderToInspect = _pathBuilder.GetFolder(currentContainerIdentifier);

                var eraFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, eraDelta);
                folderToInspect = _pathBuilder.Combine(folderToInspect, eraFolder);

                var periodFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, periodDelta);
                folderToInspect = _pathBuilder.Combine(folderToInspect, periodFolder);

                var momentFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, monentDelta);
                folderToInspect = _pathBuilder.Combine(folderToInspect, momentFolder);

                var nextContainerIdentifier = _containerProvider.ForEntry(storageFolder, accountFolder, spaceFolder, eraFolder, periodFolder, momentFolder);
                return nextContainerIdentifier;
            }
        }
    }
}
