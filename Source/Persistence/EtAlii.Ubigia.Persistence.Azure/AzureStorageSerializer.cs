﻿namespace EtAlii.Ubigia.Persistence.Azure
{
    using System;
    using System.Threading.Tasks;

    public class AzureStorageSerializer : IStorageSerializer
    {
        public string FileNameFormat { get; } = "{0}.bson";


        public Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            throw new NotImplementedException();
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            throw new NotImplementedException();
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            throw new NotImplementedException();
        }
    }
}
