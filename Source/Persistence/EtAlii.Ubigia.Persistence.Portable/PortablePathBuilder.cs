// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using System.Linq;
    using PCLStorage;

    public class PortablePathBuilder : IPathBuilder
    {
        public string BaseFolder { get; }

        private readonly IStorageSerializer _serializer;

        public PortablePathBuilder(IStorageOptions options, IStorageSerializer serializer)
        {
            _serializer = serializer;
            BaseFolder = PortablePath.Combine("EtAlii", "Ubigia", options.Name);
        }

        public string GetFolder(ContainerIdentifier container)
        {
            var relativePath = PortablePath.Combine(container.Paths);
            return PortablePath.Combine(BaseFolder, relativePath);
        }

        public string GetFileName(string fileId, ContainerIdentifier container)
        {
            var folder = GetFolder(container);
            var fileName = string.Format(_serializer.FileNameFormat, fileId);
            return PortablePath.Combine(folder, fileName);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            var parts = path.Split(PortablePath.DirectorySeparatorChar);
            var name = parts.Skip(parts.Length - 1).Single();
            var pointIndex = name.LastIndexOf('.');
            if (pointIndex != -1)
            {
                name = name.Substring(0, pointIndex);
            }
            return name;
        }

        public string Combine(string path1, string path2)
        {
            return PortablePath.Combine(path1, path2);
        }

        public string GetDirectoryName(string path)
        {
            var parts = path.Split(PortablePath.DirectorySeparatorChar);
            if (parts.Length > 1)
            {
                parts = parts.Take(parts.Length - 1).ToArray();
            }
            return string.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);
        }
    }
}
