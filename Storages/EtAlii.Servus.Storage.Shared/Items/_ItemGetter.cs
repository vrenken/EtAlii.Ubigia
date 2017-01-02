//namespace EtAlii.Servus.Storage
//{
//    using System;
//    using System.IO;
//    using System.Linq;

//    internal class ItemGetter : IItemGetter
//    {
//        private readonly IFileManager _fileManager;
//        private readonly IFolderManager _folderManager;
//        private readonly IPathBuilder _pathBuilder;
//        private readonly IItemSerializer _serializer;

//        public ItemGetter(IFileManager fileManager, IFolderManager folderManager, IPathBuilder pathBuilder, IItemSerializer serializer)
//        {
//            _fileManager = fileManager;
//            _folderManager = folderManager;
//            _pathBuilder = pathBuilder;
//            _serializer = serializer;
//        }

//        public Guid[] Get(ContainerIdentifier container)
//        {
//            var folder = _pathBuilder.GetFolder(container);
//            // Ensure that the requested folder exists.
//            _folderManager.Create(folder);

//            var searchPattern = String.Format(_serializer.FileNameFormat, "*");
//            var fileNames = _folderManager.EnumerateFiles(folder, searchPattern)
//                                            .Select(fileName => _pathBuilder.GetFileNameWithoutExtension(fileName))
//                                            .ToArray();

//            return fileNames.Select(fileName => Guid.Parse(fileName))
//                            .ToArray();
//        }
//    }
//}
