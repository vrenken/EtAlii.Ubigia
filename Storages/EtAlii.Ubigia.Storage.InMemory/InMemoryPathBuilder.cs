namespace EtAlii.Ubigia.Storage
{
    using System;
    using System.IO;

    public class InMemoryPathBuilder : IPathBuilder
    {
        public string BaseFolder { get; }

        private readonly char _separatorChar;
        private readonly string _separatorString;

        private readonly IStorageSerializer _serializer;

        public InMemoryPathBuilder(IStorageConfiguration configuration, IStorageSerializer serializer)
        {
            _separatorChar = '\\';
            _separatorString = new string(_separatorChar, 1);

            _serializer = serializer;
            BaseFolder = String.Join(_separatorString, "EtAlii");// @"\EtAlii", "Ubigia", configuration.Name);
        }

        public string GetFolder(ContainerIdentifier container)
        {
            var relativePath = String.Join(_separatorString, container.Paths);
            return String.Join(_separatorString, BaseFolder, relativePath);
        }

        public string GetFileName(string fileId, ContainerIdentifier container)
        {
            var folder = GetFolder(container);
            var fileName = String.Format(_serializer.FileNameFormat, fileId);
            return String.Join(_separatorString, folder, fileName);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string Combine(string path1, string path2)
        {
            return String.Join(_separatorString, path1, path2);
        }

        public string GetDirectoryName(string path)
        {
            var lastIndex = path.LastIndexOf(_separatorChar);
            return lastIndex == -1 ? String.Empty : path.Substring(0, lastIndex);
            //return Path.GetDirectoryName(path);
        }
    }
}
