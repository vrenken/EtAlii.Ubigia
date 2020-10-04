﻿namespace EtAlii.Ubigia.Persistence
{

    internal class BlobPartRetriever : IBlobPartRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFileManager _fileManager;

        public BlobPartRetriever(IImmutableFileManager fileManager, IPathBuilder pathBuilder)
        {
            _fileManager = fileManager;
            _pathBuilder = pathBuilder;
        }

        public T Retrieve<T>(ContainerIdentifier container, ulong position) 
            where T : BlobPartBase
        {
            var blobName = BlobPartHelper.GetName<T>();
            container = ContainerIdentifier.Combine(container, blobName);

            T blobPart = null;

            var fileName = string.Format(BlobPartStorer.FileNameFormat, position);
            
            var path = _pathBuilder.GetFileName(fileName, container);
            if (_fileManager.Exists(path))
            {
                blobPart = _fileManager.LoadFromFile<T>(path);
                BlobPartHelper.SetStored(blobPart, true);
            }

            return blobPart;
        }
    }
}