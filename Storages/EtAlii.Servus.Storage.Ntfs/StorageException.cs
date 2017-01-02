namespace EtAlii.Servus.Storage
{
    using System;

    public class StorageException : Exception
    {
        public StorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
