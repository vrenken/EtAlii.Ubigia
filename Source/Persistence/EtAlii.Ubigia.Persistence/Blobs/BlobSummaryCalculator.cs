﻿namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class BlobSummaryCalculator : IBlobSummaryCalculator
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFileManager _fileManager;

        public BlobSummaryCalculator(IPathBuilder pathBuilder, IImmutableFileManager fileManager)
        {
            _pathBuilder = pathBuilder;
            _fileManager = fileManager;
        }

        public async Task<BlobSummary> Calculate<T>(ContainerIdentifier container)
            where T: BlobBase
        {
            var summary = (BlobSummary)null;

            var blobName = BlobHelper.GetName<T>();
            container = ContainerIdentifier.Combine(container, blobName);

            const string fileName = "Blob";
            var path = _pathBuilder.GetFileName(fileName, container);
            if (_fileManager.Exists(path))
            {
                // Yup, we have a blob file. Lets load it.
                var blob = await _fileManager
                    .LoadFromFile<T>(path)
                    .ConfigureAwait(false);

                ulong totalAvailableParts = 0;
                var availableParts = new List<ulong>();
                for (ulong partId = 0; partId < blob.TotalParts; partId++)
                {
                    var partFileName = string.Format(BlobPartStorer.FileNameFormat, partId);
                    path = _pathBuilder.GetFileName(partFileName, container);
                    if (_fileManager.Exists(path))
                    {
                        availableParts.Add(partId);
                        totalAvailableParts += 1;
                    }
                }

                summary = new BlobSummary 
                {
                    IsComplete = totalAvailableParts == blob.TotalParts, 
                    TotalParts = blob.TotalParts, 
                    AvailableParts = availableParts.ToArray()
                };
            }

            return summary;
        }
    }
}
