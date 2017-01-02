namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class NextContainerIdentifierFromFolderAlgorithm : INextContainerIdentifierAlgorithm
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ILatestEntryGetter _latestEntryGetter;

        public NextContainerIdentifierFromFolderAlgorithm(IPathBuilder pathBuilder, ILatestEntryGetter latestEntryGetter)
        {
            _pathBuilder = pathBuilder;
            _latestEntryGetter = latestEntryGetter;
        }

        public ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier)
        {
            var entriesFolder = currentContainerIdentifier.Paths[0];
            var storageFolder = currentContainerIdentifier.Paths[1];
            var accountFolder = currentContainerIdentifier.Paths[2];
            var spaceFolder = currentContainerIdentifier.Paths[3];

            UInt64 eraDelta = 0;
            UInt64 periodDelta = 0;
            UInt64 monentDelta = 1;

            var folderToInspect = _pathBuilder.GetFolder(currentContainerIdentifier);

            var eraFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, eraDelta);
            folderToInspect = Path.Combine(folderToInspect, eraFolder);

            var periodFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, periodDelta);
            folderToInspect = Path.Combine(folderToInspect, periodFolder);

            var momentFolder = _latestEntryGetter.GetLatestEntry(folderToInspect, monentDelta);
            folderToInspect = Path.Combine(folderToInspect, momentFolder);

            var nextContainerIdentifier = ContainerIdentifier.FromPaths(entriesFolder, storageFolder, accountFolder, spaceFolder, eraFolder, periodFolder, momentFolder);
            return nextContainerIdentifier;
        }
    }
}
