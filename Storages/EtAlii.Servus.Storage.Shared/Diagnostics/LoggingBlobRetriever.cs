namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

    internal class LoggingBlobRetriever : IBlobRetriever
    {
        private readonly ILogger _logger;
        private readonly IBlobRetriever _blobRetriever;

        public LoggingBlobRetriever(
            ILogger logger, 
            IBlobRetriever blobRetriever)
        {
            _logger = logger;
            _blobRetriever = blobRetriever;
        }

        public T Retrieve<T>(ContainerIdentifier container) 
            where T : BlobBase
        {
            return _blobRetriever.Retrieve<T>(container);
        }
    }
}
