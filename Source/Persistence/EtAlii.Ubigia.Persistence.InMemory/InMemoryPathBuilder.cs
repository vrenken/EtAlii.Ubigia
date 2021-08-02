// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.IO;

    public class InMemoryPathBuilder : IPathBuilder
    {
        public string BaseFolder { get; }

        private readonly char _separatorChar;

        private readonly IStorageSerializer _serializer;

        public InMemoryPathBuilder(IStorageSerializer serializer)
        {
            _separatorChar = Path.DirectorySeparatorChar;

            _serializer = serializer;
            BaseFolder = string.Join(_separatorChar, "EtAlii");
        }

        public string GetFolder(ContainerIdentifier container)
        {
            var relativePath = string.Join(_separatorChar, container.Paths);
            return string.Join(_separatorChar, BaseFolder, relativePath);
        }

        public string GetFileName(string fileId, ContainerIdentifier container)
        {
            var folder = GetFolder(container);
            var fileName = string.Format(_serializer.FileNameFormat, fileId);
            return string.Join(_separatorChar, folder, fileName);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string Combine(string path1, string path2)
        {
            return string.Join(_separatorChar, path1, path2);
        }

        public string GetDirectoryName(string path)
        {
            var lastIndex = path.LastIndexOf(_separatorChar);
            return lastIndex == -1 ? string.Empty : path.Substring(0, lastIndex);
        }
    }
}
