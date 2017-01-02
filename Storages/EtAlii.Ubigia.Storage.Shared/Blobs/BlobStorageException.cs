namespace EtAlii.Ubigia.Storage
{
    using System;

    public class BlobStorageException : Exception
    {
        public BlobStorageException(string message)
            : base(message)
        {
        }

        public BlobStorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
