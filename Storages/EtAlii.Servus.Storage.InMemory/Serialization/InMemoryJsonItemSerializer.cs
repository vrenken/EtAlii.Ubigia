namespace EtAlii.Servus.Storage
{
    using Newtonsoft.Json;
    using System.IO;
    using EtAlii.Servus.Api.Transport;

    public class InMemoryJsonItemSerializer : IItemSerializer
    {
        private readonly ISerializer _serializer;
        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.json";

        public InMemoryJsonItemSerializer(
            ISerializer serializer, 
            IInMemoryItemsHelper inMemoryItemsHelper)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            File file;
            var stream = _inMemoryItemsHelper.CreateFile(fileName, out file);
            {
                using (var textWriter = new StreamWriter(stream))
                {
                    using (var writer = new JsonTextWriter(textWriter))
                    {
                        _serializer.Serialize(writer, item);
                    }
                }
            }
            file.Content = stream.ToArray();
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            T item = null;

            using (var stream = _inMemoryItemsHelper.OpenFile(fileName))
            {
                using (var textReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(textReader))
                    {
                        item = _serializer.Deserialize<T>(reader);
                    }
                }
            }
            return item;
        }
    }
}
