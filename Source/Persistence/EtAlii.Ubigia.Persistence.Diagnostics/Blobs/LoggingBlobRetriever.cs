namespace EtAlii.Ubigia.Persistence
{
    internal class LoggingBlobRetriever : IBlobRetriever
    {
        private readonly IBlobRetriever _blobRetriever;

        public LoggingBlobRetriever(IBlobRetriever blobRetriever)
        {
            _blobRetriever = blobRetriever;
        }

        public T Retrieve<T>(ContainerIdentifier container) 
            where T : BlobBase
        {
            return _blobRetriever.Retrieve<T>(container);
        }
    }
}
