namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class BlobStorageException : Exception
    {
        private BlobStorageException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

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
