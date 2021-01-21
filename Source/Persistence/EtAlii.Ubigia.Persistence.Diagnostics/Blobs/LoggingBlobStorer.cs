namespace EtAlii.Ubigia.Persistence
{
    using Serilog;

    internal class LoggingBlobStorer : IBlobStorer
    {
        private readonly IBlobStorer _decoree;
        private readonly IPathBuilder _pathBuilder;
        private readonly ILogger _logger = Log.ForContext<IBlobStorer>();

        public LoggingBlobStorer(
            IBlobStorer decoree,
            IPathBuilder pathBuilder)
        {
            _decoree = decoree;
            _pathBuilder = pathBuilder;
        }

        public void Store(ContainerIdentifier container, Blob blob)
        {
            var blobName = Blob.GetName(blob);
            var logContainer = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(logContainer);

            _logger.Verbose("Storing {BlobName} blob in: {Folder}", blobName, folder);

            _decoree.Store(container, blob);
        }
    }
}
