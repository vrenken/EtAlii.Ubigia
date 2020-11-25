﻿namespace EtAlii.Ubigia.Persistence.Ntfs
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Microsoft.Experimental.IO;

    public class NtfsStorageSerializer : IStorageSerializer
    {
        private readonly IItemSerializer _itemSerializer;
        private readonly IPropertiesSerializer _propertiesSerializer;

        public string FileNameFormat { get; } = "{0}.bson";

        public NtfsStorageSerializer(
            IItemSerializer itemSerializer, 
            IPropertiesSerializer propertiesSerializer)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            using var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.Write);
            
            _itemSerializer.Serialize(stream, item);
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            using var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.Write);
            
            _propertiesSerializer.Serialize(stream, item);
        }

        public Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            using var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            return _itemSerializer.Deserialize<T>(stream);
        }
        
        public PropertyDictionary Deserialize(string fileName)
        {
            using var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            return _propertiesSerializer.Deserialize(stream);
        }

    }
}
