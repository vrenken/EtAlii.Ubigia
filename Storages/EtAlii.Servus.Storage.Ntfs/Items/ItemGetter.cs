namespace EtAlii.Servus.Storage
{
    using System;
    using System.IO;
    using System.Linq;

    public class ItemGetter : IItemGetter
    {
        private readonly IFolderManager _folderManager;
        private readonly IPathBuilder _pathBuilder;
        private readonly IItemSerializer _serializer;

        public ItemGetter(IFolderManager folderManager, IPathBuilder pathBuilder, IItemSerializer serializer)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
            _serializer = serializer;
        }

        public Guid[] Get(ContainerIdentifier container)
        {
            var folder = _pathBuilder.GetFolder(container);
            // Ensure that the requested folder exists.
            LongPathHelper.Create(folder);

            var searchPattern = String.Format(_serializer.FileNameFormat, "*");
            var fileNames = _folderManager.EnumerateFiles(folder, searchPattern)
                                          .Select(fileName => Path.GetFileNameWithoutExtension(fileName))
                                          .ToArray();

            return fileNames.Select(fileName => Guid.Parse(fileName))
                            .ToArray();
        }
    }
}
