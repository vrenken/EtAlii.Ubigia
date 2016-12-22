namespace EtAlii.Servus.Storage
{
    using System;
    using EtAlii.Servus.Api;

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

        public void Store(ContainerIdentifier container, IBlob blob)
        {
            if (container == null)
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

        public void Store(ContainerIdentifier container, IBlobPart blobPart)
        {
            if (container == null)
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

        public T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase
        {
            if (container == null)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                return _blobRetriever.Retrieve<T>(container);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to retrieve blob from the specified container", e);
            }
        }

        public T Retrieve<T>(ContainerIdentifier container, UInt64 position)
            where T : BlobPartBase
        {
            if (container == null)
            {
                throw new BlobStorageException("No container specified");
            }

            try
            {
                return _blobPartRetriever.Retrieve<T>(container, position);
            }
            catch (Exception e)
            {
                throw new BlobStorageException("Unable to retrieve blob part from the specified container", e);
            }
        }
    }
}
