namespace EtAlii.Ubigia.Persistence
{
    using System;
    using EtAlii.Ubigia.Api;

    internal class LoggingBlobPartRetriever : IBlobPartRetriever
    {
        private readonly IBlobPartRetriever _blobPartRetriever;

        public LoggingBlobPartRetriever(IBlobPartRetriever blobPartRetriever)
        {
            _blobPartRetriever = blobPartRetriever;
        }

        public T Retrieve<T>(ContainerIdentifier container, UInt64 position) 
            where T : BlobPartBase
        {
            return _blobPartRetriever.Retrieve<T>(container, position);
        }
    }
}
