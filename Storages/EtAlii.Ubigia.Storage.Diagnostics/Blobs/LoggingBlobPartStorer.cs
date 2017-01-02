namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;

    internal class LoggingBlobPartStorer : IBlobPartStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IBlobPartStorer _blobPartStorer;
        private readonly ILogger _logger;

        public LoggingBlobPartStorer(
            ILogger logger,
            IBlobPartStorer blobPartStorer,
            IPathBuilder pathBuilder)
        {
            _logger = logger;
            _blobPartStorer = blobPartStorer;
            _pathBuilder = pathBuilder;
        }

        public void Store(ContainerIdentifier container, IBlobPart blobPart)
        {
            var blobName = BlobPartHelper.GetName(blobPart);
            var logContainer = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(logContainer);

            _logger.Verbose("Storing {0} blob part in: {1}", blobName, folder);

            _blobPartStorer.Store(container, blobPart);
        }
    }
}
