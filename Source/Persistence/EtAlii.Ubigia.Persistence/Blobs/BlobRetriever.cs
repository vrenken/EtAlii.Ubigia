// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    internal class BlobRetriever : IBlobRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;
        private readonly IBlobSummaryCalculator _blobSummaryCalculator;

        public BlobRetriever(IImmutableFolderManager folderManager,
                             IPathBuilder pathBuilder,
                             IBlobSummaryCalculator blobSummaryCalculator)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;

            _blobSummaryCalculator = blobSummaryCalculator;
        }

        public async Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : Blob
        {
            var blobName = Blob.GetName<T>();
            var blobContainer = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(blobContainer);

            T blob = null;

            if (_folderManager.Exists(folder))
            {
                blob = await _folderManager
                    .LoadFromFolder<T>(folder, "Blob")
                    .ConfigureAwait(false);
                Blob.SetStored(blob, true);

                var summary = await _blobSummaryCalculator
                    .Calculate<T>(container)
                    .ConfigureAwait(false);
                Blob.SetSummary(blob, summary);
            }

            return blob;
        }
    }
}
