namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Threading.Tasks;

    internal class BlobStorage : IBlobStorage
    {
        private readonly IBlobStorer _blobStorer;
        private readonly IBlobRetriever _blobRetriever;

        private readonly IBlobPartStorer _blobPartStorer;
        private readonly IBlobPartRetriever _blobPartRetriever;

        public BlobStorage(IBlobStorer blobStorer, 
                           IBlobRetriever blobRetriever,
                           IBlobPartStorer blobPartStorer,
                           IBlobPartRetriever blobPartRetriever)
        {
            _blobStorer = blobStorer;
            _blobRetriever = blobRetriever;

            _blobPartStorer = blobPartStorer;
            _blobPartRetriever = blobPartRetriever;
        }

        public void Store(ContainerIdentifier container, BlobBase blob)
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                _blobStorer.Store(container, blob);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to store blob in the specified container", e);
            }
        }

        public void Store(ContainerIdentifier container, BlobPartBase blobPart)
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                _blobPartStorer.Store(container, blobPart);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to store blob part in the specified container", e);
            }
        }

        public async Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                return await _blobRetriever
                    .Retrieve<T>(container)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to retrieve blob from the specified container", e);
            }
        }

        public async Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPartBase
        {
            if (container == ContainerIdentifier.Empty)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                return await _blobPartRetriever
                    .Retrieve<T>(container, position)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to retrieve blob part from the specified container", e);
            }
        }
    }
}
