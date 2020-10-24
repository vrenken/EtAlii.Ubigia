namespace EtAlii.Ubigia.Persistence
{
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

        public T Retrieve<T>(ContainerIdentifier container) 
            where T : BlobBase
        {
            var blobName = BlobHelper.GetName<T>();
            var blobContainer = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(blobContainer);

            T blob = null;

            if (_folderManager.Exists(folder))
            {
                blob = _folderManager.LoadFromFolder<T>(folder, "Blob");
                BlobHelper.SetStored(blob, true);

                var summary = _blobSummaryCalculator.Calculate<T>(container);
                BlobHelper.SetSummary(blob, summary);
            }

            return blob;
        }
    }
}
