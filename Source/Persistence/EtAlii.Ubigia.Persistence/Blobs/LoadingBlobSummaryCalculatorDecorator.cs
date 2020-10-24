namespace EtAlii.Ubigia.Persistence
{
    internal class LoadingBlobSummaryCalculatorDecorator : IBlobSummaryCalculator
    {
        private readonly IBlobSummaryCalculator _blobSummaryCalculator;
        private readonly IPathBuilder _pathBuilder;
        private readonly IFileManager _fileManager;

        public LoadingBlobSummaryCalculatorDecorator(IFileManager fileManager, IPathBuilder pathBuilder, IBlobSummaryCalculator blobSummaryCalculator)
        {
            _fileManager = fileManager;
            _pathBuilder = pathBuilder;
            _blobSummaryCalculator = blobSummaryCalculator;
        }

        public BlobSummary Calculate<T>(ContainerIdentifier container)
            where T : BlobBase
        {
            BlobSummary summary;

            var blobName = BlobHelper.GetName<T>();
            var blobContainer = ContainerIdentifier.Combine(container, blobName);

            const string fileName = "Summary";
            var path = _pathBuilder.GetFileName(fileName, blobContainer);
            if (_fileManager.Exists(path))
            {
                // Yup, we have a summary file. Lets load it.
                summary = _fileManager.LoadFromFile<BlobSummary>(path);
            }
            else
            {
                // Nope, the summary file is not yet available. Lets calculate the summary.
                summary = _blobSummaryCalculator.Calculate<T>(container);

                if (summary != null && summary.IsComplete)
                {
                    // Ok, the blob is complete. Lets write the summary to disk so that all future access is as fast as possible.
                    _fileManager.SaveToFile(path, summary);
                }
            }
            return summary;
        }
    }
}
