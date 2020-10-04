namespace EtAlii.Ubigia.Persistence
{
    using System;

    internal class BlobStorer : IBlobStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;

        public BlobStorer(IImmutableFolderManager folderManager, 
                          IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }
        
        public void Store(ContainerIdentifier container, IBlob blob)
        {
            var blobName = BlobHelper.GetName(blob);
            container = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(container);

            _folderManager.Create(folder);

            BlobHelper.SetStored(blob, false);
            BlobHelper.SetSummary(blob, null);
            _folderManager.SaveToFolder(blob, "Blob", folder);
            BlobHelper.SetStored(blob, true);
            BlobHelper.SetSummary(blob, BlobSummary.Create(false, blob, new UInt64[] { }));
        }
    }
}
