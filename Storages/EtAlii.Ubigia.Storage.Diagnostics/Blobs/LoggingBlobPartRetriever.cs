namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;

    internal class LoggingBlobPartRetriever : IBlobPartRetriever
    {
        private readonly IBlobPartRetriever _blobPartRetriever;
        private readonly ILogger _logger;

        public LoggingBlobPartRetriever(
            ILogger logger, 
            IBlobPartRetriever blobPartRetriever)
        {
            _logger = logger;
            _blobPartRetriever = blobPartRetriever;
        }

        public T Retrieve<T>(ContainerIdentifier container, UInt64 position) 
            where T : BlobPartBase
        {
            return _blobPartRetriever.Retrieve<T>(container, position);
        }
    }
}
