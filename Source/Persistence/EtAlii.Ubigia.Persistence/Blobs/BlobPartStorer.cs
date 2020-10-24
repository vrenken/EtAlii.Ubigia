namespace EtAlii.Ubigia.Persistence
{
    internal class BlobPartStorer : IBlobPartStorer
    {
        public const string FileNameFormat = "Part_{0}";

        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;

        public BlobPartStorer(IImmutableFolderManager folderManager, 
                              IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }

        public void Store(ContainerIdentifier container, IBlobPart blobPart)
        {
            var blobName = BlobPartHelper.GetName(blobPart);
            container = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(container);

            _folderManager.Create(folder);

            var fileName = string.Format(FileNameFormat, blobPart.Id);

            BlobPartHelper.SetStored(blobPart, false);
            _folderManager.SaveToFolder(blobPart, fileName, folder);
            BlobPartHelper.SetStored(blobPart, true);
        }
    }
}
