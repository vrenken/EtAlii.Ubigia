namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    [Serializable]
    public class EntryRepositoryException : Exception
    {
        public EntryRepositoryException()
        {
        }

        public EntryRepositoryException(string message)
            : base(message)
        {
        }

        public EntryRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}