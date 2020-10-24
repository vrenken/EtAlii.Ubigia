namespace EtAlii.Ubigia.Persistence.Azure
{
    using System;

    public partial class AzureFileManager : IFileManager
    {
        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            throw new NotImplementedException();
        }

        public T LoadFromFile<T>(string path)
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
