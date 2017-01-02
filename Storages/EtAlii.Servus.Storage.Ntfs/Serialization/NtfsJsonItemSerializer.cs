namespace EtAlii.Servus.Storage.Ntfs
{
    using Microsoft.Experimental.IO;
    using Newtonsoft.Json;
    using System.IO;
    using EtAlii.Servus.Api.Transport;

    public class NtfsJsonItemSerializer : IItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.json";

        public NtfsJsonItemSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.Write))
            {
                using (var textWriter = new StreamWriter(stream))
                {
                    using (var writer = new Newtonsoft.Json.JsonTextWriter(textWriter))
                    {
                        _serializer.Serialize(writer, item);
                    }
                }
            }
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            T item = null;

            using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(stream))
                {
                    using (var reader = new Newtonsoft.Json.JsonTextReader(textReader))
                    {
                        item = _serializer.Deserialize<T>(reader);
                    }
                }
            }
            return item;
        }
    }
}
