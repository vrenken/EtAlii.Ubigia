namespace EtAlii.Servus.Storage.Azure
{
    using Newtonsoft.Json;
    using System;
    using EtAlii.Servus.Api.Transport;

    public class AzureJsonItemSerializer : IItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.json";

        public AzureJsonItemSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            throw new NotImplementedException();
            //using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.ReadWrite))
            //{
            //    using (var textWriter = new StreamWriter(stream))
            //    {
            //        using (var writer = new Newtonsoft.Json.JsonTextWriter(textWriter))
            //        {
            //            _serializer.Serialize(writer, item);
            //        }
            //    }
            //}
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            throw new NotImplementedException();
            //T item = null;

            //using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            //{
            //    using (var textReader = new StreamReader(stream))
            //    {
            //        using (var reader = new Newtonsoft.Json.JsonTextReader(textReader))
            //        {
            //            item = _serializer.Deserialize<T>(reader);
            //        }
            //    }
            //}
            //return item;
        }
    }
}
