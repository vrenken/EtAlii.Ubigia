namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;

    internal class BlobPartRetriever : IBlobPartRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;
        private readonly IImmutableFileManager _fileManager;

        public BlobPartRetriever(IImmutableFileManager fileManager,
                                 IImmutableFolderManager folderManager, 
                                 IPathBuilder pathBuilder)
        {
            _fileManager = fileManager;
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }

        public T Retrieve<T>(ContainerIdentifier container, UInt64 position) 
            where T : BlobPartBase
        {
            var blobName = BlobPartHelper.GetName<T>();
            container = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(container); // TODO: What does this line do? Or what should it do?

            T blobPart = null;

            var fileName = String.Format(BlobPartStorer.FileNameFormat, position);
            
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
