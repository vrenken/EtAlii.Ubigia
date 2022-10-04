// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.Threading.Tasks;

    public class InMemoryStorageSerializer : IStorageSerializer
    {
        private readonly IItemSerializer _itemSerializer;
        private readonly IPropertiesSerializer _propertiesSerializer;

        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public string FileNameFormat { get; } = "{0}.bin";

        public InMemoryStorageSerializer(
            IItemSerializer itemSerializer,
            IPropertiesSerializer propertiesSerializer,
            IInMemoryItemsHelper inMemoryItemsHelper)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }

        public void Serialize<T>(string fileName, T item)
            where T : class
        {
            using var stream = _inMemoryItemsHelper.CreateFile(fileName, out var file);

            _itemSerializer.Serialize(stream, item);
            file.Content = stream.ToArray();
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            using var stream = _inMemoryItemsHelper.CreateFile(fileName, out var file);

            _propertiesSerializer.Serialize(stream, item);
            file.Content = stream.ToArray();
        }

        public async Task<T> Deserialize<T>(string fileName)
            where T : class
        {
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var stream = _inMemoryItemsHelper.OpenFile(fileName);
#pragma warning restore CA2007
            return await _itemSerializer.Deserialize<T>(stream).ConfigureAwait(false);
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            using var stream = _inMemoryItemsHelper.OpenFile(fileName);

            return _propertiesSerializer.Deserialize(stream);
        }
    }
}
