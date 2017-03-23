namespace EtAlii.Ubigia.Storage.Azure
{
    using System;
    using System.IO;

    public class AzurePathBuilder : IPathBuilder
    {
        public string BaseFolder { get; }

        private readonly IStorageSerializer _serializer;

        public AzurePathBuilder(IStorageConfiguration configuration, IStorageSerializer serializer)
        {
            _serializer = serializer;
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            BaseFolder = Path.Combine(folder, "EtAlii", "Ubigia", configuration.Name);
        }

        public string GetFolder(ContainerIdentifier container)
        {
            throw new NotImplementedException();
            //var relativePath = Path.Combine(container.Paths);
            //return Path.Combine(_baseFolder, relativePath);
        }

        public string GetFileName(string fileId, ContainerIdentifier container)
        {
            throw new NotImplementedException();
            //var folder = GetFolder(container);
            //var fileName = String.Format(_serializer.FileNameFormat, fileId);
            //return Path.Combine(folder, fileName);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            throw new NotImplementedException();
            //return Path.GetFileNameWithoutExtension(path);
        }

        public string Combine(string path1, string path2)
        {
            throw new NotImplementedException();
            //return Path.Combine(path1, path2);
        }

        public string GetDirectoryName(string path)
        {
            throw new NotImplementedException();
            //return Path.GetDirectoryName(path);
        }
    }
}
