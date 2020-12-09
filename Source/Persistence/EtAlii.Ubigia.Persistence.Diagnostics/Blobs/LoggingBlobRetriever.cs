﻿namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    internal class LoggingBlobRetriever : IBlobRetriever
    {
        private readonly IBlobRetriever _blobRetriever;

        public LoggingBlobRetriever(IBlobRetriever blobRetriever)
        {
            _blobRetriever = blobRetriever;
        }

        public Task<T> Retrieve<T>(ContainerIdentifier container) 
            where T : Blob
        {
            return _blobRetriever.Retrieve<T>(container);
        }
    }
}
