// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure
{
    using System;
    using System.Threading.Tasks;

    public partial class AzureFileManager : IFileManager
    {
        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadFromFile<T>(string path)
            where T : class
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }
    }
}
