namespace EtAlii.Servus.Storage
{
    using System;

    public class ComponentStorageException : Exception
    {
        public ComponentStorageException(string message)
            : base(message)
        {
        }

        public ComponentStorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
