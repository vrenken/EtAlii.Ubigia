namespace EtAlii.Ubigia.Persistence.Ntfs
{
    using System;
    using System.IO;

    public class NtfsPathBuilder : IPathBuilder
    {
        public string BaseFolder { get; private set; }

        private readonly IStorageConfiguration _configuration;
        private readonly IStorageSerializer _serializer;
        private readonly char _separatorChar;
        private readonly string _separatorString;

        public NtfsPathBuilder(
            IStorageConfiguration configuration, 
            IStorageSerializer serializer)
        {
            _separatorChar = Path.DirectorySeparatorChar;
            _separatorString = new string(_separatorChar, 1);

            _configuration = configuration;
            _serializer = serializer;
        }

        public void Initialize(string baseFolder)
        {
            var folder = Environment.ExpandEnvironmentVariables(baseFolder);
            BaseFolder = String.Join(_separatorString, folder, _configuration.Name);
        }

        public string GetFolder(ContainerIdentifier container)
        {
            var relativePath = String.Join(_separatorString, container.Paths);
            return Path.Combine(BaseFolder, relativePath);
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
        }
    }
}
