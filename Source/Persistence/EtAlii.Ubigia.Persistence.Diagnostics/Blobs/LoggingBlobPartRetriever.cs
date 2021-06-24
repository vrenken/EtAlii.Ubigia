// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    internal class LoggingBlobPartRetriever : IBlobPartRetriever
    {
        private readonly IBlobPartRetriever _blobPartRetriever;

        public LoggingBlobPartRetriever(IBlobPartRetriever blobPartRetriever)
        {
            _blobPartRetriever = blobPartRetriever;
        }

        public Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPart
        {
            return _blobPartRetriever.Retrieve<T>(container, position);
        }
    }
}
