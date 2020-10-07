namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;

    internal class LoggingBlobStorer : IBlobStorer
    {
        private readonly IBlobStorer _blobStorer;
        private readonly IPathBuilder _pathBuilder;
        private readonly ILogger _logger;

        public LoggingBlobStorer(
            ILogger logger, 
            IBlobStorer blobStorer,
            IPathBuilder pathBuilder)
        {
            _logger = logger;
            _blobStorer = blobStorer;
            _pathBuilder = pathBuilder;
        }
        
        public void Store(ContainerIdentifier container, IBlob blob)
        {
            var blobName = BlobHelper.GetName(blob);
            var logContainer = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(logContainer);

            _logger.Verbose("Storing {0} blob in: {1}", blobName, folder);

            _blobStorer.Store(container, blob);
        }
    }
}
